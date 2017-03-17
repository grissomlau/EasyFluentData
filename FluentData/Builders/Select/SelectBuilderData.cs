using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentData
{
    public class SelectBuilderData : BuilderData
    {
        public int PagingCurrentPage { get; set; }
        public int PagingItemsPerPage { get; set; }
        public string Having { get; set; }
        public string GroupBy { get; set; }
        public string OrderBy { get; set; }
        public string From { get; set; }
        public string Select { get; set; }
        /// <summary>
        /// Dh 2014-04-23 添加 distinct 关键字
        /// </summary>
        public string Distinct { get; set; }

        // begin  dh 2015-02-09 
        // 由于 SqlServerProvider 对当前页码为 1 时用了简便的分页 top 来提高性能, 而不为 1 时用了 with 的临时表，但 rownumber order by 语句不能用别名列，而复杂的sql 中 order by 却只能用别名，所以当有复杂的 order by 时可分别设置；
        private string _rowNumberOrderBy = null;
        public string RowNumberOrderBy {
            get {
                if (string.IsNullOrEmpty(_rowNumberOrderBy)) {
                    _rowNumberOrderBy = this.OrderBy;
                }
                return _rowNumberOrderBy;
            }
            set {
                _rowNumberOrderBy = value;
            }
        }

        /// <summary>
        /// added by grissom on 2016-03-14 mysql 返回分页前的查询行数
        /// </summary>
        private bool _hasFoundRows = false;
        public bool HasFoundRows{
            get {
                return _hasFoundRows;
            }
            set {
                _hasFoundRows = value;
            }
        }

        public string WhereSql { get; set; }

        public SelectBuilderData(IDbCommand command, string objectName)
            : base(command, objectName) {
            Having = "";
            GroupBy = "";
            OrderBy = "";
            RowNumberOrderBy = "";
            From = "";
            Select = "";
            WhereSql = "";
            PagingCurrentPage = 1;
            PagingItemsPerPage = 0;
        }

        internal int GetFromItems() {
            return (GetToItems() - PagingItemsPerPage + 1);
        }

        internal int GetToItems() {
            return (PagingCurrentPage * PagingItemsPerPage);
        }
    }
}
