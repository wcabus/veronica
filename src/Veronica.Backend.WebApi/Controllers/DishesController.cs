using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Veronica.Backend.Application.Dishes;
using Veronica.Backend.Domain.Models;
using V1 = Veronica.Backend.ApiModels.V1.Dishes;

namespace Veronica.Backend.WebApi.Controllers
{
    /// <summary>
    /// Dishes API controller
    /// </summary>
    [Produces("application/json")]
    [Route("dishes")]
    public class DishesController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all dishes for the user identified by <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of dishes.</returns>
        [HttpGet("~/users/{userId:guid}/dishes")]
        [SwaggerResponse(200, typeof(IEnumerable<V1.Dish>), "The list of dishes.")]
        [SwaggerResponse(404, Description = "The user doesn't exist.")]
        public async Task<IActionResult> GetDishesForUser(Guid userId)
        {
            var response = await _mediator.Send(new GetDishesForUser(userId));
            if (response == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<IEnumerable<V1.Dish>>(response));
        }

        /// <summary>
        /// Returns the dish identified by <paramref name="dishId"/> and <paramref name="userId"/> (if provided).
        /// </summary>
        /// <param name="dishId">The ID of the dish.</param>
        /// <param name="userId">Optional. The ID of the user.</param>
        /// <returns>A dish.</returns>
        [HttpGet("~/users/{userId:guid}/dishes/{dishId:guid}")]
        [HttpGet("{dishId:guid}", Name = Routes.GetDishById)]
        [SwaggerResponse(200, typeof(V1.Dish), "The dish.")]
        [SwaggerResponse(404, Description = "The user doesn't exist when userId has been provided, or the dish doesn't exist.")]
        public async Task<IActionResult> GetDishForUser(Guid dishId, Guid? userId = null)
        {
            Dish response;

            if (userId == null)
            {
                var query = new GetDish(dishId);
                response = await _mediator.Send(query);
            }
            else
            {
                var query = new GetDishForUser(userId.Value, dishId);
                response = await _mediator.Send(query);
            }

            if (response == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<V1.Dish>(response));
        }

        /// <summary>
        /// Adds a new dish to the user's dish collection.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="model"></param>
        /// <returns>The new dish.</returns>
        [HttpPost("~/users/{userId:guid}/dishes")]
        [SwaggerResponse(201, typeof(V1.Dish), "The new dish.")]
        [SwaggerResponse(400, Description = "No or invalid data has been provided.")]
        [SwaggerResponse(404, Description = "The user doesn't exist.")]
        public async Task<IActionResult> AddDish(Guid userId, [FromBody] V1.CreateDish model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var request = Mapper.Map<CreateDish>(model);
            request.UserId = userId;

            await _mediator.Send(request);

            return CreatedAtRoute(
                Routes.GetDishById,
                new { dishId = request.Id },
                Mapper.Map<V1.Dish>(request));
        }

        /// <summary>
        /// Removes the dish from the user's dish collection.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="dishId">The ID of the dish.</param>
        /// <returns>Nothing.</returns>
        [HttpDelete("~/users/{userId:guid}/dishes/{dishId:guid}")]
        [SwaggerResponse(204, Description = "The delete has succeeded.")]
        [SwaggerResponse(404, Description = "The user or the dish doesn't exist.")]
        public async Task<IActionResult> RemoveDish(Guid userId, Guid dishId)
        {
            var request = new RemoveDish(userId, dishId);
            await _mediator.Send(request);

            if (request.Succeeded)
            {
                return NoContent();
            }

            return NotFound();
        }

        private static class Routes
        {
            public const string GetDishById = nameof(GetDishById);
        }
    }
}