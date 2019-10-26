using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <author>刘迪</author>
    /// <createtime>2019-10-22</createtime>
    /// <updator></updator>
    /// <updatetime></updatetime>
    /// <description></description>
    public class AccountController : Controller
    {
        protected static Log.ILog m_log = Log.LogFactory.GetDefaultInstance();

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody]Models.User.UserLoginInfoViewModel userInfo)
        {
            Util.Return.ReturnValue<String> returnValue = new Util.Return.ReturnValue<String>();
            try
            {
                //if (ModelState.IsValid)
                //{
                bool result = true;
                //使用用户名到数据库获取用户信息（需要考虑用户状态）
                //验证密码是否正确
                if (result)
                {
                    Int32 expired = 1;
                    if (userInfo.RememberMe) expired = 30;
                    DateTime expiredTime = System.DateTime.UtcNow.AddDays(expired);
                    HttpContext.Response.Cookies.Append(
                        Util.LoginTokenHelper.LoginTokenName
                        , Util.LoginTokenHelper.GetToken(userInfo.UserName, expiredTime)
                        , new Microsoft.AspNetCore.Http.CookieOptions()
                        {
                            Expires = expiredTime
                        });

                    returnValue.IsOperateSuccess = true;
                    returnValue.Description = "Success";
                }
                else
                {
                    returnValue.IsOperateSuccess = false;
                    returnValue.Description = "Username or password is incorrect, please re-enter.";
                }
                //}


            }
            catch (Exception ex)
            {
                returnValue.IsOperateSuccess = false;
                returnValue.Description = "The server is abnormal, please try again later.";

                m_log.Error("Login fail.", ex);
            }
            return new JsonResult(returnValue);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Signout()
        {

            Util.Return.ReturnValue<String> returnValue = new Util.Return.ReturnValue<String>();
            try
            {
                //删除相关登录信息（特别是Cookie）
                HttpContext.Response.Cookies.Delete(Util.LoginTokenHelper.LoginTokenName);

                returnValue.IsOperateSuccess = true;
                returnValue.Description = "success";
            }
            catch (Exception ex)
            {
                returnValue.IsOperateSuccess = false;
                returnValue.Description = "The server is abnormal, please try again later.";

                m_log.Error("Sign out fail.", ex);
            }
            return new JsonResult(returnValue);
        }

    }
}