using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Controllers
{

    /* Copyright:dx.com
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
    /// 
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


    }
}
