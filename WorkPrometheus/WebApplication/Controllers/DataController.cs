namespace WebApplication.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using WebApplication.Accessor;

    public class DataController : Controller
    {
        private readonly IDataAccessor dataAccessor;

        public DataController(IDataAccessor dataAccessor)
        {
            this.dataAccessor = dataAccessor;
        }

        public async ValueTask<IActionResult> Index()
        {
            return View(await dataAccessor.QueryDataListAsync());
        }

        public async ValueTask<IActionResult> Detail(long id)
        {
            var entity = await dataAccessor.QueryDataAsync(id);
            if (entity is null)
            {
                return NotFound();
            }

            return View(entity);
        }
    }
}
