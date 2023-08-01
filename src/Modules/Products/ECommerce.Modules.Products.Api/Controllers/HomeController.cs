using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Modules.Products.Api.Controllers;

[Route(ProductsModule.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get() => "Product API";
}