namespace WebApplication.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using WebApplication.Services;

    /// <summary>
    ///
    /// </summary>
    public class DataResponse
    {
        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private IMapper Mapper { get; }

        private DataService DataService { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dataService"></param>
        public DataController(IMapper mapper, DataService dataService)
        {
            Mapper = mapper;
            DataService = dataService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DataResponse> Get()
        {
            return DataService.QueryDataList().Select(Mapper.Map<DataResponse>);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = DataService.QueryData(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<DataResponse>(DataService.QueryData(id)));
        }
    }
}
