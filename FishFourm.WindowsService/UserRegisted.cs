using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.WindowsService
{
    /// <summary>
    /// 用户注册消息契约
    /// </summary>
    public interface UserRegisted
    {
        string Id { get; set; }

        string UserName { get; set; }
    }
}
