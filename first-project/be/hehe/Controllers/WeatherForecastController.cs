using hehe.Models;
using Microsoft.AspNetCore.Mvc;

namespace hehe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static List<Product> products = new()
    {
        new Product{Id=1, Name="hehe1"},
        new Product{Id=2, Name="hehe2"}
    };

    public static List<Product> Products => products;

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(products);
    }
}
