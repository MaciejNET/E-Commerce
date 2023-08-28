using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Returns.Api.Controllers;

[Route(ReturnsModule.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get() => "Returns API";
}