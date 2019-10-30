using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Controllers
{

    /* Copyright: liudi
     *
     * Autor: xx
     * Create Time: xx
     *
     * Rewriter: liudi
     * Rewriter Time: 2019.10.12
     * Description: xx
     *
     * Rewriter: xx
     * Rewriter Time: xx
     * Description: xx
     * 
     * 
     * **/

    /// <summary>
    /// 控制器基类
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <author>刘迪</author>
    /// <createtime>2019-10-12</createtime>
    /// <updator></updator>
    /// <updatetime></updatetime>
    /// <description></description>
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            
        }

        protected static Log.ILog m_log = Log.LogFactory.GetDefaultInstance();

        //protected bool IsLogin(String arg)
        //{
        //    String strToken = HttpContext.Request.Cookies["MSToken"];
        //    if (!String.IsNullOrWhiteSpace(strToken) && !Common.LoginTokenHelper.IsExpired(strToken))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private String m_userName = null;
        protected String UserName
        {
            get
            {
                if (null == m_userName)
                {
                    String strToken = HttpContext.Request.Cookies[Util.LoginTokenHelper.LoginTokenName];
                    m_userName = Util.LoginTokenHelper.GetUserName(strToken);
                }
                return m_userName;
            }
        }

        /// <summary>
        /// 执行方法之后
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            var result = context.Result;
            // Do something with Result.
            if (context.Canceled == true)
            {
                // Action execution was short-circuited by another filter.
            }

            if (context.Exception != null)
            {
                StringBuilder sbErrorDescription = new StringBuilder();
                sbErrorDescription.Append($"Error Controller:{context.Controller.ToString()}");
                sbErrorDescription.Append($"\r\n ActionDescriptor:{context.ActionDescriptor.DisplayName.ToString()}");
                sbErrorDescription.Append($"\r\n Exception.Message:{context.Exception.Message.ToString()}");
                m_log.Error(sbErrorDescription.ToString(), context.Exception);

                // Exception thrown by action or action filter.
                // Set to null to handle the exception.
                context.Exception = null;
            }
            base.OnActionExecuted(context);
        }

        /// <summary>
        /// 执行方法之前，进行登录验证，
        /// 如不需要类中的所有方法都验证用户是否登录，不要继承该类
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.

            String strToken = context.HttpContext.Request.Cookies[Util.LoginTokenHelper.LoginTokenName];
            if (String.IsNullOrWhiteSpace(strToken))
            {
                //context.Result = new BadRequestObjectResult("Please login first.");
                context.Result = new Manager.Controllers.LoginController().Index();
            }


        }


    }
}
