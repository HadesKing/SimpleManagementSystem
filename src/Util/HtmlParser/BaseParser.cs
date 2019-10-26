using System;
using System.Collections.Generic;
using System.Text;

namespace Util.HtmlParser
{

    /// <summary>
    /// 解析者基类
    /// </summary>
    public abstract class BaseParser
    {

        /// <summary>
        /// 处理节点路径
        /// </summary>
        /// <param name="argNodePath"></param>
        /// <returns></returns>
        protected String ProcessNodePath(String argNodePath)
        {
            //获取节点路径（\将会被替换成/）
            String strPath = String.IsNullOrWhiteSpace(argNodePath) ? "" : argNodePath.Replace("\\", "/");

            return strPath;
        }

    }
}
