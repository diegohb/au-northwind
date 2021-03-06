namespace Presentation.WebSPA.ApiControllers;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Threading.Tasks;
using ApiConfig;
using Infra.Persistence.EF;
using Infra.Persistence.EF.Entities;
using Infra.Persistence.EF.Entities.QueryViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
[SuppressMessage("ReSharper", "RouteTemplates.RouteParameterIsNotPassedToMethod")]
public class ProductsController : ControllerBase
{
    private readonly NorthwindDbContext _northwindDb;

    public ProductsController(NorthwindDbContext dbContextParam)
    {
        _northwindDb = dbContextParam;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IList<Product>>> GetAll()
    {
        var productEntities = await _northwindDb.Products.AsNoTracking().ToListAsync();
        if (productEntities.Count == 0)
        {
            return NoContent();
        }

        return Ok(productEntities);
    }

    [HttpGet("bycategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IList<ProductsByCategoryView>>> GetByCategory()
    {
        var productEntities = await _northwindDb.ProductsByCategories.AsNoTracking().ToListAsync();
        if (productEntities.Count == 0)
        {
            return NoContent();
        }

        return Ok(productEntities);
    }

    /// <summary>
    ///     Fetch product by sku.
    /// </summary>
    /// <param name="skuParam">The guid that represents the sku for the product.</param>
    /// <returns></returns>
    [HttpGet("{sku:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerDefaultValue("sku", "94b14c55-f76d-ac18-e366-697742b93469")]
    public async Task<ActionResult<Product>> GetBySku([FromRoute(Name = "sku")] Guid skuParam)
    {
        var productEntity = await _northwindDb.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Sku.Equals(skuParam));
        if (productEntity == null)
        {
            return NotFound();
        }

        return Ok(productEntity);
    }
}