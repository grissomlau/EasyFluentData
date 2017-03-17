namespace FluentData
{
    internal class DeleteBuilder : BaseDeleteBuilder, IDeleteBuilder
    {
        public DeleteBuilder(IDbCommand command, string tableName)
            : base(command, tableName) {
        }

        public IDeleteBuilder Where(string columnName, object value, DataTypes parameterType, int size) {
            Actions.ColumnValueAction(columnName, value, parameterType, size);
            return this;
        }
        public IDeleteBuilder AddWhere(string where) {
            Actions.AddWhereAction(where);
            return this;
        }
        public IDeleteBuilder AddWhereParam(string paramName, object value) {
            Actions.AddWhereParamAction(paramName, value);
            return this;
        }
    }
}
