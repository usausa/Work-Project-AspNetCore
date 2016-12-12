namespace WebApplication.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Dapper;

    using WebApplication.Infrastructure.Data;
    using WebApplication.Models;

    /// <summary>
    ///
    /// </summary>
    public class DataService
    {
        private IConnectionFactory ConnectionFactory { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="connectionFactory"></param>
        public DataService(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IList<DataEntity> QueryDataList()
        {
            return ConnectionFactory.Using(
                con => con.Query<DataEntity>("SELECT * FROM data ORDER BY id", buffered: false).ToList());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataEntity QueryData(int id)
        {
            return ConnectionFactory.Using(
                con => con.Query<DataEntity>("SELECT * FROM data ORDER BY id", buffered: false).FirstOrDefault());
        }
    }
}
