namespace Presentation.WebSPA.ApiControllers;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Threading.Tasks;
using ApiConfig;
using ErrorOr;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<CatalogProductDTO>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetAllProductsQuery());
        return result.MatchFirst<IActionResult>
        (products => Ok(products),
            error => error.Type == ErrorType.NotFound ? NoContent() : Problem(error.Description));
    }

    [HttpGet("/api/categories/{categoryName}/products")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CatalogProductDTO))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetByCategory([FromRoute(Name = "categoryName")] string categoryNameParam)
    {
        var result = await _sender.Send(new GetProductsByCategoryQuery(categoryNameParam));
        return result.MatchFirst<IActionResult>
        (product => Ok(product),
            error => error.Type == ErrorType.NotFound ? NoContent() : Problem(error.Description));
    }

    /// <summary>
    ///     Fetch product by sku.
    /// </summary>
    /// <param name="skuParam">The guid that represents the sku for the product.</param>
    /// <returns></returns>
    [HttpGet("{sku:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CatalogProductDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerDefaultValue("sku", "94b14c55-f76d-ac18-e366-697742b93469")]
    public async Task<IActionResult> GetBySku([FromRoute(Name = "sku")] Guid skuParam)
    {
        var result = await _sender.Send(new GetProductBySkuQuery(skuParam));
        return result.MatchFirst<IActionResult>
            (Ok, error => error.Type == ErrorType.NotFound ? NoContent() : Problem(error.Description));
    }

    /// <summary>
    ///     Update product price by sku.
    /// </summary>
    /// <param name="dtoParam">Request paramters object.</param>
    /// <returns></returns>
    [HttpPut("{sku:guid}/price")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerDefaultValue("sku", "94b14c55-f76d-ac18-e366-697742b93469")]
    public async Task<IActionResult> UpdateProductPrice(ProductPriceChangeRequestDTO dtoParam)
    {
        var result = await _sender.Send
            (new UpdateProductPriceBySkuCommand(dtoParam.ProductSku, dtoParam.OriginalPrice, dtoParam.ChangeAmount, dtoParam.Comment));
        return result.MatchFirst<IActionResult>
        (product => Ok(),
            error => error.Type == ErrorType.NotFound ? NotFound() : Problem(error.Description));
    }

    public record ProductPriceChangeRequestDTO
        ([FromRoute(Name = "sku")] Guid ProductSku, [FromBody] decimal OriginalPrice, [FromBody] decimal ChangeAmount, [FromBody] string Comment);
}