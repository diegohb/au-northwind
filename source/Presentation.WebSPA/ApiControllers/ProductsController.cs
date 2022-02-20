﻿namespace Presentation.WebSPA.ApiControllers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infra.Persistence.EF;
using Infra.Persistence.EF.Entities;
using Infra.Persistence.EF.Entities.QueryViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
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
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<IList<ProductsByCategoryView>>> GetByCategory()
    {
        var productEntities = await _northwindDb.ProductsByCategories.AsNoTracking().ToListAsync();
        if (productEntities.Count == 0)
        {
            return NoContent();
        }

        return Ok(productEntities);
    }

    [HttpGet("{skuParam:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IList<Product>>> GetBySku(Guid skuParam)
    {
        var productEntity = await _northwindDb.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Sku.Equals(skuParam));
        if (productEntity == null)
        {
            return NotFound();
        }

        return Ok(productEntity);
    }
}