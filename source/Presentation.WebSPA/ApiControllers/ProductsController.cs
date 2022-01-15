namespace Presentation.WebSPA.ApiControllers;

using System.Collections.Generic;
using System.Threading.Tasks;
using Infra.Persistence.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scaffolded.Entities;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly NorthwindDbContext _northwindDb;

    public ProductsController(NorthwindDbContext dbContextParam)
    {
        _northwindDb = dbContextParam;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var productEntities = await _northwindDb.Products.AsNoTracking().ToListAsync();
        if (productEntities.Count == 0)
        {
            return NoContent();
        }

        return Ok(productEntities);
    }
}