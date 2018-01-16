
using Abp;
using FishFourm.Application.Users;
using FishFourm.WindowsService.Consumers;
using GreenPipes;
using MassTransit;
using MassTransit.Util;
using System;
using Topshelf;

namespace FishFourm.WindowsService
{
    class UserService : ServiceControl
    {
        private  AbpBootstrapper _abpBootstrapper;
        
        IBusControl _bus;

        public bool Start(HostControl hostControl)
        {
            _bus = ConfigureBus();
            // _bus.Start();
            TaskUtil.Await(() => _bus.StartAsync());
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _bus?.Stop(TimeSpan.FromSeconds(30));
            return true;
        }

        IBusControl ConfigureBus()
        {
            using (_abpBootstrapper = AbpBootstrapper.Create<FishFourmWinServiceModule>())
            {
                _abpBootstrapper.Initialize();
                return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                var service = _abpBootstrapper.IocManager.Resolve<IUserAppService>();

                cfg.ReceiveEndpoint(host, "userRegisted_queue", e =>
                {       
                    //每隔5秒尝试消费一次
                    e.UseRetry(retryConfig => retryConfig.Interval(5,TimeSpan.FromSeconds(5)));
                    e.Consumer<UserRegistedComsumer>(() => new UserRegistedComsumer(service));
                    e.Consumer<UserUpdatedConsumer>(() => new UserUpdatedConsumer(service));
                });

                //cfg.ReceiveEndpoint(host, "userUpdated_queue", e =>
                //{
                //    //每隔5秒尝试消费一次
                //    e.UseRetry(retryConfig => retryConfig.Interval(5, TimeSpan.FromSeconds(5)));   
                //    e.Consumer<UserUpdatedConsumer>(() => new UserUpdatedConsumer(service));
                //});
            });
            }
        }
    }
}
