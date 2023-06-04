namespace Presentation.WebSPA.ApiControllers;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Threading.Tasks;
using ApiConfig;
using Infra.Persistence.EF.Entities;
using Infra.Persistence.EF.Entities.QueryViews;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Products;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
[SuppressMessage("ReSharper", "RouteTemplates.RouteParameterIsNotPassedToMethod")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsController(ISender senderParam)
    {
        _sender = senderParam;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IList<Product>>> GetAll()
    {
        var result = await _sender.Send(new GetAllProductsQuery());
        if (result.Count == 0)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpGet("bycategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IList<ProductsByCategoryView>>> GetByCategory()
    {
        /*var productEntities = await _northwindDb.ProductsByCategories.AsNoTracking().ToListAsync();
        if (productEntities.Count == 0)
        {
            return NoContent();
        }

        return Ok(productEntities);*/
        await Task.Delay(1000);
        return NoContent();
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
        var result = await _sender.Send(new GetProductBySku(skuParam));
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}