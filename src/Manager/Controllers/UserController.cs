using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 注册用户信息
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {

            return View();
        }

    }
}