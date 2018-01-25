 
(function ($) {
    //1.得到$.ajax的对象
    var _ajax = $.ajax;
    $.ajax = function (options) {
        var cookie = getCookie("token");
        //2.每次调用发送ajax请求的时候定义默认的error处理方法
        var fn = {
            error: function (e) {
                if (e.status == 401) {
                    var authorizedEnpoint = "http://localhost:56995/Users/Login";
                    var return_url = location.href;
                    var client_id = '97a9b860-0692-4d10-9103-5b301886b0cf';
                    var grant_type = 'password';
                    location.href = authorizedEnpoint
                        + "?redirect_uri=" + return_url + ""
                        + "&client_Id=" + client_id + ""
                        + "&grant_type=" + grant_type + "";
                }
            },
            success: function (data, textStatus) { },
            beforeSend: function (XHR) { },
            complete: function (XHR, TS) { },
            headers:
            {
                'Authorization': 'bearer ' + cookie
            }
        }
        //3.如果在调用的时候写了error的处理方法，就不用默认的
        if (options.error) {
            fn.error = options.error;
        }
        if (options.success) {
            fn.success = options.success;
        }
        if (options.beforeSend) {
            fn.beforeSend = options.beforeSend;
        }
        if (options.complete) {
            fn.complete = options.complete;
        }
        //4.扩展原生的$.ajax方法，返回最新的参数
        var _options = $.extend(options, {
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                fn.error(XMLHttpRequest, textStatus, errorThrown);
            },
            success: function (data, textStatus) {
                fn.success(data, textStatus);
            },
            beforeSend: function (XHR) {
                fn.beforeSend(XHR);
            },
            complete: function (XHR, TS) {
                fn.complete(XHR, TS);
            },
            headers: fn.headers
        });
        //5.将最新的参数传回ajax对象
        _ajax(_options);
    };
})(jQuery);

// 写cookies
function setCookie(name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
};

//读取cookies
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");

    if (arr = document.cookie.match(reg))

        return unescape(arr[2]);
    else
        return null;
};

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
};

 