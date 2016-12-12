namespace WebApplication.Api
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using WebApplication.Models;
    using WebApplication.Services;

    /// <summary>
    ///
    /// </summary>
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private DataService DataService { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataService"></param>
        public DataController(DataService dataService)
        {
            DataService = dataService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DataEntity> Get()
        {
            return DataService.QueryDataList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public DataEntity Get(int id)
        {
            return DataService.QueryData(id);
        }
    }
}
