using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models
{

    /// <summary>
    /// 分页
    /// </summary>
    public class Pagination
    {

        /// <summary>
        /// 总条数
        /// </summary>
        public Int32 TotalRow { get; set; }

        /// <summary>
        /// 当前页索引
        /// </summary>
        public Int32 CurrentPageIndex { get; set; }

        private Int32 m_pageSize = 10;
        /// <summary>
        /// 一页多少条
        /// </summary>
        public Int32 PageSize
        {
            get
            {
                return m_pageSize;
            }
            set
            {
                if (value > 0) m_pageSize = value;
                m_pageSize = value;
            }
        }

        private Int32 m_totalPage = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public Int32 TotalPage
        {
            get
            {
                if (m_totalPage == 0)
                {
                    Int32 remainder = TotalRow % PageSize;
                    Int32 tmp = TotalRow / PageSize;
                    m_totalPage = remainder > 0 ? ++tmp : tmp;
                }
                
                return m_totalPage;
            }
        }

        /// <summary>
        /// 获取数据的URL
        /// </summary>
        public String Url { get; set; }

    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Pagination<T> : Pagination
    {

        /// <summary>
        /// 当前页数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }

    }
}
