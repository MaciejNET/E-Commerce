using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Users.Api.Controllers;

[Route(UsersModule.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get() => "Users API";
}