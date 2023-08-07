using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Discounts.Api.Controllers;

[Route(DiscountsModule.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get() => "Discounts API";
}