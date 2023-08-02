using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Reviews.Api.Controllers;

[Route(ReviewsModule.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get() => "Reviews API";
}