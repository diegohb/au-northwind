namespace Presentation.WebSPA.ApiControllers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infra.Persistence.EF;
using Infra.Persistence.EF.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly NorthwindDbContext _northwindDb;

    public CategoriesController(NorthwindDbContext dbContextParam)
    {
        _northwindDb = dbContextParam;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<IList<Category>>> GetAll()
    {
        var productCategoryEntities = await _northwindDb.Categories.AsNoTracking().ToListAsync();
        if (productCategoryEntities.Count == 0)
        {
            return NoContent();
        }

        return Ok(productCategoryEntities);
    }

    [HttpGet("{idParam:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IList<Product>>> GetByID(int idParam)
    {
        var categoryEntity = await _northwindDb.Categories.AsNoTracking().SingleOrDefaultAsync(category => category.CategoryId.Equals(idParam));

        if (categoryEntity == null)
        {
            return NotFound();
        }

        return Ok(categoryEntity);
    }


    [HttpGet("{nameParam}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IList<Product>>> GetByName(string nameParam)
    {
        var decodedName = Uri.UnescapeDataString(nameParam);
        var categoryEntity = await _northwindDb.Categories.AsNoTracking().SingleOrDefaultAsync(category => category.CategoryName.Equals(decodedName));

        if (categoryEntity == null)
        {
            return NotFound();
        }

        return Ok(categoryEntity);
    }
}