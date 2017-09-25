namespace Application.Services
{
    using System.Threading.Tasks;

    using Application.Models.Entity;

    using Dapper;

    using Smart.Data;

    public class AccountService
    {
        private IConnectionFactory ConnectionFactory { get; }

        public AccountService(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public Task<AccountEntity> QueryAccount(string id)
        {
            return ConnectionFactory.UsingAsync(con =>
                con.QueryFirstOrDefaultAsync<AccountEntity>(
                    "SELECT * FROM Account WHERE Id = @id",
                    new { id }));
        }
    }
}
