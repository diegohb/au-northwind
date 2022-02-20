namespace Presentation.WebSPA.ApiControllers;

using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Infra.Persistence.EF;
using Infra.Persistence.EF.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly NorthwindDbContext _northwindDb;

    public ProductsController(NorthwindDbContext dbContextParam)
    {
        _northwindDb = dbContextParam;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll()
    {
        var productEntities = await _northwindDb.Products.AsNoTracking().ToListAsync();
        if (productEntities.Count == 0)
        {
            return NoContent();
        }

        return Ok(productEntities);
    }

    [HttpGet("bycategory")]
    [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetByCategory()
    {
        var productEntities = await _northwindDb.ProductsByCategories.AsNoTracking().ToListAsync();
        if (productEntities.Count == 0)
        {
            return NoContent();
        }

        return Ok(productEntities);
    }

    [HttpGet("{skuParam:guid}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBySku(Guid skuParam)
    {
        var productEntity = await _northwindDb.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Sku.Equals(skuParam));
        if (productEntity == null)
        {
            return NotFound();
        }

        return Ok(productEntity);
    }
}