namespace Application.Services
{
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;

    using Application.Models;
    using Application.Models.Entity;

    using Dapper;

    using Smart.Data;

    public class MasterService
    {
        private IConnectionFactory ConnectionFactory { get; }

        private IDialect Dialect { get; }

        public MasterService(
            IConnectionFactory connectionFactory,
            IDialect dialect)
        {
            ConnectionFactory = connectionFactory;
            Dialect = dialect;
        }

        public Task<Paged<ItemEntity>> QueryItemPaged(Pageable parameter)
        {
            return ConnectionFactory.UsingAsync(async con =>
            {
                var reader = await con.QueryMultipleAsync(
                    "SELECT * FROM Item ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;" +
                    "SELECT COUNT(*) FROM Item",
                    parameter);
                var list = (await reader.ReadAsync<ItemEntity>(false)).ToList();
                var count = (await reader.ReadAsync<int>(false)).First();
                return Paged.From(parameter, list, count);
            });
        }

        public Task<bool> InsertItem(ItemEntity entity)
        {
            return ConnectionFactory.UsingAsync(async con =>
            {
                try
                {
                    await con.ExecuteAsync(
                        "INSERT INTO Item (" +
                        "Code, " +
                        "Name, " +
                        "Price, " +
                        "UpdatedAt" +
                        ") VALUES (" +
                        "@Code, " +
                        "@Name, " +
                        "@Price, " +
                        "CURRENT_TIMESTAMP" +
                        ")",
                        entity);

                    return true;
                }
                catch (DbException e)
                {
                    if (Dialect.IsDuplicate(e))
                    {
                        return false;
                    }

                    throw;
                }
            });
        }

        public Task<int> UpdateItem(ItemEntity entity)
        {
            return ConnectionFactory.UsingAsync(con =>
                con.ExecuteAsync(
                    "UPDATE Item SET " +
                    "Name = @Name, " +
                    "Price = @Price, " +
                    "UpdatedAt = CURRENT_TIMESTAMP " +
                    "WHERE Code = @Code",
                    entity));
        }

        public Task<int> DeleteItem(string code)
        {
            return ConnectionFactory.UsingAsync(con =>
                con.ExecuteAsync(
                    "DELETE Item WHERE Code = @Code",
                    new { Code = code }));
        }
    }
}
