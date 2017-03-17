using System;
using System.Collections.Generic;
using System.Data;

namespace FluentData
{
    public interface ISelectBuilder<TEntity>
    {
        SelectBuilderData Data { get; set; }
        ISelectBuilder<TEntity> Select(string sql);
        ISelectBuilder<TEntity> From(string sql);
        ISelectBuilder<TEntity> Where(string sql);
        ISelectBuilder<TEntity> AndWhere(string sql);
        ISelectBuilder<TEntity> OrWhere(string sql);
        ISelectBuilder<TEntity> GroupBy(string sql);
        ISelectBuilder<TEntity> OrderBy(string sql);
        ISelectBuilder<TEntity> Having(string sql);
        /// <summary>
        /// Dh 2014-04-23 Add distinct key word
        /// </summary>
        /// <returns></returns>
        ISelectBuilder<TEntity> Distinct();
        ///dh 2015-02-9
        /// 由于 SqlServerProvider 对当前页码为 1 时用了简便的分页 top 来提高性能, 而不为 1 时用了 with 的临时表，但 rownumber order by 语句不能用别名列，而复杂的sql 中 order by 却只能用别名，所以当有复杂的 order by 时可分别设置； 
        /// 只在 SqlServerProvider 中实现
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        ISelectBuilder<TEntity> RowNumberOrderBy(string sql);

        /// <summary>
        /// added by grissom on 2016-03-14 mysql 返回未分页前的记录行数
        /// </summary>
        /// <returns></returns>
        ISelectBuilder<TEntity> FoundRows();

        ISelectBuilder<TEntity> Paging(int currentPage, int itemsPerPage);
        ISelectBuilder<TEntity> Parameter(string name, object value, DataTypes parameterType = DataTypes.Object, ParameterDirection direction = ParameterDirection.Input, int size = 0);
        ISelectBuilder<TEntity> Parameters(params object[] parameters);

        List<TEntity> QueryMany(Action<TEntity, IDataReader> customMapper = null);
        List<TEntity> QueryMany(Action<TEntity, dynamic> customMapper);
        TList QueryMany<TList>(Action<TEntity, IDataReader> customMapper = null) where TList : IList<TEntity>;
        TList QueryMany<TList>(Action<TEntity, dynamic> customMapper) where TList : IList<TEntity>;
        void QueryComplexMany(IList<TEntity> list, Action<IList<TEntity>, IDataReader> customMapper);
        void QueryComplexMany(IList<TEntity> list, Action<IList<TEntity>, dynamic> customMapper);
        TEntity QuerySingle(Action<TEntity, IDataReader> customMapper = null);
        TEntity QuerySingle(Action<TEntity, dynamic> customMapper);
        TEntity QueryComplexSingle(Func<IDataReader, TEntity> customMapper);
        TEntity QueryComplexSingle(Func<dynamic, TEntity> customMapper);
    }
}
