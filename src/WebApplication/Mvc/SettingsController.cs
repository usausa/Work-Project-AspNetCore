namespace WebApplication.Mvc
{
    using Microsoft.AspNetCore.Mvc;

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
        public SettingsController(SmtpSettings settings)
        {
            Settings = settings;
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
