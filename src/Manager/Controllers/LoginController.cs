using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{

    /// <summary>
    /// 登录
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <author>刘迪</author>
    /// <createtime>2019-10-22</createtime>
    /// <updator></updator>
    /// <updatetime></updatetime>
    /// <description></description>
    public class LoginController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //这里需要进行校验，是否有Cookie
            //Log.LogFactory.GetDefaultInstance().Debug("123456");        //TODO:测试代码
            String strToken = HttpContext.Request.Cookies[Util.LoginTokenHelper.LoginTokenName];
            if (!String.IsNullOrWhiteSpace(strToken) && !Util.LoginTokenHelper.IsExpired(strToken))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


    }
}