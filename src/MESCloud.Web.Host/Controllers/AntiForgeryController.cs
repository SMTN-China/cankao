using Microsoft.AspNetCore.Antiforgery;
using MESCloud.Controllers;

namespace MESCloud.Web.Host.Controllers
{
    public class AntiForgeryController : MESCloudControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
