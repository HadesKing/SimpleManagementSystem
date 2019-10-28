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


        /// <summary>
        /// 验证用户输入数据
        /// </summary>
        /// <param name="argUserLoginInfo"></param>
        /// <returns></returns>
        private (bool, String) ValidateData(Models.User.UserLoginInfoViewModel argUserLoginInfo)
        {
            bool result = true;
            String strErrorMsg = null;

            if (null == argUserLoginInfo
                || String.IsNullOrWhiteSpace(argUserLoginInfo.UserName)
                || String.IsNullOrWhiteSpace(argUserLoginInfo.Password))
            {
                result = false;
                strErrorMsg = "Please enter your username and password.";
            }
            else
            {
                Int32 userNameLength = argUserLoginInfo.UserName.Length;
                Int32 passwordNameLength = argUserLoginInfo.Password.Length;
                if (userNameLength < 5 || userNameLength > 16)
                {
                    result = false;
                    strErrorMsg = "Username can only be 5-16 characters.";
                }
                else if (passwordNameLength < 6 || passwordNameLength > 20)
                {
                    result = false;
                    strErrorMsg = "Username can only be 6-20 characters.";
                }
            }

            return (result, strErrorMsg);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userInfo">输入用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login([FromBody]Models.User.UserLoginInfoViewModel userInfo)
        {
            Util.Return.ReturnResult returnValue = new Util.Return.ReturnResult();
            try
            {
                bool validateResult;
                String strErrorMsg = null;
                (validateResult, strErrorMsg) = ValidateData(userInfo);
                if (validateResult)
                {
                    //使用用户名到数据库获取用户信息
                    DataBll.User.UserInfoDataBll userInfoDataBll = new DataBll.User.UserInfoDataBll();
                    Model.User.UserInfo user = userInfoDataBll.Get(userInfo.UserName);
                    if (null != user
                        && user.State == Model.EnumType.UserState.Normal
                        && !String.IsNullOrWhiteSpace(userInfo.Password))
                    {
                        String strPassword = Util.EncryptionAlgorithm.Md5Algorithm.Encryption32(userInfo.Password);
                        if (strPassword == user.Password)
                        {
                            //生成Token
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
                            returnValue.Description = "Wrong password, please re-enter.";
                        }
                    }
                    else
                    {
                        returnValue.Description = "This user not exist.Please re-enter your username and password.";
                    }
                }
                else
                {
                    returnValue.Description = strErrorMsg;
                }
            }
            catch (Exception ex)
            {
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
        //[Produces("application/json")]
        public IActionResult Signout()
        {
            Util.Return.ReturnValue<String> returnValue = new Util.Return.ReturnValue<String>();
            try
            {
                //删除相关登录信息（特别是Cookie）
                HttpContext.Response.Cookies.Delete(Util.LoginTokenHelper.LoginTokenName);

                returnValue.IsOperateSuccess = true;
                returnValue.Description = "Success";
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