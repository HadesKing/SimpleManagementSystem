using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class UserPasswordController : BaseController
    {

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "修改密码";
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit()
        {
            Util.Return.ReturnValue<String> returnValue = new Util.Return.ReturnValue<String>();
            try
            {

            }
            catch (Exception ex)
            {
                returnValue.IsOperateSuccess = false;
                returnValue.Description = "The server is abnormal, please try again later.";

                m_log.Error("Edit password fail.", ex);
            }

            return new JsonResult(returnValue);
        }

    }
}