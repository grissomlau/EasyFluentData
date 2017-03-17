namespace FluentData.Providers.Common.Builders
{
    internal class DeleteBuilderSqlGenerator
    {
        public string GenerateSql(IDbProvider provider, string parameterPrefix, BuilderData data) {
            var whereSql = "";
            foreach (var column in data.Columns) {
                if (whereSql.Length > 0)
                    whereSql += " and ";

                whereSql += string.Format("{0} = {1}{2}",
                                                provider.EscapeColumnName(column.ColumnName),
                                                parameterPrefix,
                                                column.ParameterName);
            }
            //dh 2015-03-14 add where string
            if (data.AdditionalWhere.Length > 0) {
                if (string.IsNullOrEmpty(whereSql))
                    whereSql += " 1=1 ";
                whereSql += data.AdditionalWhere.ToString();
            }

            var sql = string.Format("delete from {0} where {1}", data.ObjectName, whereSql);

            return sql;
        }
    }
}
