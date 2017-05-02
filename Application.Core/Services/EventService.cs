namespace Application.Services
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Application.Domain;
    using Application.Models;
    using Application.Models.Entity;
    using Application.Models.View;

    using Dapper;

    using Smart.Data;

    public class EventSearchParameter : Pageable
    {
        public DateTimeOffset From { get; set; }

        public DateTimeOffset To { get; set; }

        public EventType? EventType { get; set; }
    }

    public class EventService
    {
        private IConnectionFactory ConnectionFactory { get; }

        public EventService(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public Task<Paged<EventEntity>> QueryEventPaged(EventSearchParameter parameter)
        {
            var sql = new StringBuilder(256);
            var condition = new StringBuilder(64);
            var parameters = new DynamicParameters();

            condition.Append(" WHERE EventAt >= @From AND EventAt < @To");
            parameters.Add("From", parameter.From);
            parameters.Add("To", parameter.To);
            if (parameter.EventType.HasValue)
            {
                condition.Append(" AND EventType = @EventType");
                parameters.Add("EventType", parameter.EventType.Value);
            }

            sql.Append("SELECT * FROM Event");
            sql.Append(condition);
            sql.Append(" ORDER BY EventAt DESC OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY");
            parameters.Add("Offset", parameter.Offset);
            parameters.Add("Size", parameter.Size);

            sql.Append("; ");

            sql.Append("SELECT COUNT(*) FROM Event");
            sql.Append(condition);

            return ConnectionFactory.UsingAsync(async con =>
            {
                var reader = await con.QueryMultipleAsync(sql.ToString(), parameters);
                var list = (await reader.ReadAsync<EventEntity>(false)).ToList();
                var count = (await reader.ReadAsync<int>(false)).First();
                return Paged.From(parameter, list, count);
            });
        }

        public Task<EventSummaryView> QueryEventSummary()
        {
            return ConnectionFactory.Using(con =>
                con.QueryFirstAsync<EventSummaryView>("SELECT * FROM EventSummary"));
        }
    }
}
