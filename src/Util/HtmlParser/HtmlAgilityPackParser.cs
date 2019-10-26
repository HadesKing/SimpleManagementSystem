using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace Util.HtmlParser
{

    /// <summary>
    /// Html Agility Pack 解析
    /// 
    /// 
    /// </summary>
    public sealed class HtmlAgilityPackParser : BaseParser
    {

        /// <summary>
        /// 获取网址HTML内容
        /// </summary>
        /// <param name="argUrl"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public String LoadFromUrl(String argUrl, Encoding encoding)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(argUrl);
            return htmlDoc.Text;
        }

        /// <summary>
        /// 获取批量节点内容
        /// </summary>
        /// <param name="argHtmlConent">html字符串</param>
        /// <param name="argNodeXPath">节点路径（相对于根节点，例如：root/parentnode/childnode）</param>
        /// <param name="isInnerHtml">是否获取节点内部信息（如果是false，将会获取节点信息）</param>
        /// <returns></returns>
        public IList<String> GetNodesContent(String argHtmlConent, String argNodeXPath, bool isInnerHtml = true)
        {
            IList<String> nodesContentList = new List<String>();
            //1. 加载html信息
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(argHtmlConent);
            //2. 处理节点路径
            String strPath = ProcessNodePath(argNodeXPath);
            //3. 获取节点
            HtmlNodeCollection htmlNodes = htmlDoc.DocumentNode.SelectNodes(String.Format("{0}", argNodeXPath));
            //4. 获取节点信息
            if (null != htmlNodes && htmlNodes.Count > 0)
            {
                if (isInnerHtml)
                {
                    foreach (HtmlNode tmpNode in htmlNodes)
                    {
                        nodesContentList.Add(tmpNode.InnerHtml);
                    }
                }
                else
                {
                    foreach (HtmlNode tmpNode in htmlNodes)
                    {
                        nodesContentList.Add(tmpNode.OuterHtml);
                    }
                }
                
            }
            return nodesContentList;
        }

        /// <summary>
        /// 获取批量节点属性
        /// </summary>
        /// <param name="argHtmlConent">html字符串</param>
        /// <param name="argNodeXPath">节点路径（相对于根节点，例如：root/parentnode/childnode）</param>
        /// <param name="argAttributeName">属性名称</param>
        /// <returns></returns>
        public IList<String> GetNodesAttributeConent(String argHtmlConent, String argNodeXPath, String argAttributeName)
        {
            IList<String> attributeContentList = new List<String>();
            //1. 加载html信息
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(argHtmlConent);
            //2. 处理节点路径
            String strPath = ProcessNodePath(argNodeXPath);
            //3. 获取节点
            HtmlNodeCollection htmlNodes = htmlDoc.DocumentNode.SelectNodes(String.Format("{0}", argNodeXPath));
            //4. 获取节点信息
            if (null != htmlNodes && htmlNodes.Count > 0)
            {
                foreach (HtmlNode tmpNode in htmlNodes)
                {
                    attributeContentList.Add(tmpNode.GetAttributeValue(argAttributeName, ""));
                }

            }
            return attributeContentList;
        }


    }
}
