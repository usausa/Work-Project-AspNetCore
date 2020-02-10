namespace WebApplication.Accessor
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using WebApplication.Models;

    using Smart.Data.Accessor.Attributes;

    [DataAccessor]
    public interface IDataAccessor
    {
        [Query]
        ValueTask<List<DataEntity>> QueryDataListAsync();

        [QueryFirstOrDefault]
        ValueTask<DataEntity> QueryDataAsync(long id);
    }
}
