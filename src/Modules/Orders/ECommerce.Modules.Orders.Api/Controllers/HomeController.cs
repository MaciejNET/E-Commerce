using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Orders.Api.Controllers;

[Route(OrdersModule.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get() => "Orders API";
}