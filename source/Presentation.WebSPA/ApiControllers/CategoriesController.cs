namespace Presentation.WebSPA.ApiControllers;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Threading.Tasks;
using ApiConfig;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Categories;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
[SuppressMessage("ReSharper", "RouteTemplates.RouteParameterIsNotPassedToMethod")]
public class CategoriesController : ControllerBase
{
    private readonly ISender _sender;

    public CategoriesController(ISender senderParam)
    {
        _sender = senderParam;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IList<CatalogCategoryDTO>>> GetAll()
    {
        var result = await _sender.Send(new GetCategoriesQuery());
        if (result.Count == 0)
        {
            return NoContent();
        }

        return Ok(result);
    }

    /// <summary>
    ///     Fetch category by ID.
    /// </summary>
    /// <param name="idParam">ID for category to be fetched.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerDefaultValue("id", "4")]
    public async Task<ActionResult<CatalogCategoryDTO>> GetByID([FromRoute(Name = "id")] int idParam)
    {
        var categoryEntity = await _sender.Send(new GetCategoryByIDQuery(idParam));

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
    [HttpGet("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerDefaultValue("name", "Dairy Products")]
    public async Task<ActionResult<CatalogCategoryDTO>> GetByName([FromRoute(Name = "name")] string nameParam)
    {
        var decodedName = Uri.UnescapeDataString(nameParam);
        var categoryEntity = await _sender.Send(new GetCategoryByNameQuery(decodedName));

        if (categoryEntity == null)
        {
            return NotFound();
        }

        return Ok(categoryEntity);
    }
}