namespace WebApplication.Mvc
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    using WebApplication.Settings;

    /// <summary>
    ///
    /// </summary>
    public class SettingsController : Controller
    {
        private SmtpSettings Settings { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="settings"></param>
        public SettingsController(IOptions<SmtpSettings> settings)
        {
            Settings = settings.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(Settings);
        }
    }
}
