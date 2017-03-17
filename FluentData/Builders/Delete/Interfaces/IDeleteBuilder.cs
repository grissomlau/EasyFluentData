namespace FluentData
{
    public interface IDeleteBuilder : IExecute
    {
        BuilderData Data { get; }
        IDeleteBuilder Where(string columnName, object value, DataTypes parameterType = DataTypes.Object, int size = 0);
        // dh 2015-03-14 add where string
        IDeleteBuilder AddWhere(string where);
        IDeleteBuilder AddWhereParam(string paramName, object value);
    }
}