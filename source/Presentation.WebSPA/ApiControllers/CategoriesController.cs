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
    [Consumes("application/json")]
    [Produces("application/json")]
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

    /// <summary>
    ///     Fetch category by ID.
    /// </summary>
    /// <param name="idParam">ID for category to be fetched.</param>
    /// <returns></returns>
    [HttpGet("{idParam:int}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Category>> GetByID(int idParam)
    {
        var categoryEntity = await _northwindDb.Categories.AsNoTracking().SingleOrDefaultAsync(category => category.CategoryId.Equals(idParam));

        if (categoryEntity == null)
        {
            return NotFound();
        }

        return Ok(categoryEntity);
    }

    /// <summary>
    ///     Fetch category by unique name.
    /// </summary>
    /// <param name="nameParam">The name of the category to fetch.</param>
    /// <returns></returns>
    [HttpGet("{nameParam}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Category>> GetByName(string nameParam)
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