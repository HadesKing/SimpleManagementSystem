using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.CustomeFilter
{
    /*
     * 资料：https://docs.microsoft.com/zh-cn/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.0#action-filters
     * 方法说明：
     *     必须登录才能执行的操作，需要使用该类
     */
    /// <summary>
    /// 必须登录才能执行的操作，需要使用该类
    /// 也可以在创建一个BaseController 覆盖 OnActionExecuting 和 OnActionExecuted 方法
    /// 
    /// </summary>
    public class LoginValidateAttribute : ActionFilterAttribute
    {
        private static Log.ILog m_log = Log.LogFactory.GetDefaultInstance();
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
