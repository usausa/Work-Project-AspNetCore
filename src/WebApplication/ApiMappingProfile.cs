namespace WebApplication
{
    using AutoMapper;

    using WebApplication.Api;
    using WebApplication.Models;

    /// <summary>
    ///
    /// </summary>
    public class ApiMappingProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public ApiMappingProfile()
        {
            CreateMap<DataEntity, DataResponse>();
        }
    }
}
