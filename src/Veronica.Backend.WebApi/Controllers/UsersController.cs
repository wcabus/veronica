using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Veronica.Backend.Application.Users;
using V1 = Veronica.Backend.ApiModels.V1.Users;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Veronica.Backend.WebApi.Controllers
{
    /// <summary>
    /// Users API controller
    /// </summary>
    [Produces("application/json")]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet]
        [SwaggerResponse(200, typeof(IEnumerable<V1.User>), "A list of all users.")]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _mediator.Send(new GetUsers());

            if (response == null)
            {
                return Ok();
            }

            return Ok(Mapper.Map<IEnumerable<V1.User>>(response));
        }

        /// <summary>
        /// Returns the user matching the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>A user</returns>
        [HttpGet("{id:guid}", Name = Routes.GetUserById)]
        [SwaggerResponse(200, typeof(V1.User), "The user.")]
        [SwaggerResponse(404, Description = "No user exists for the given id.")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var response = await _mediator.Send(new GetUserById(id));

            if (response == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<V1.User>(response));
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The newly registered user.</returns>
        [HttpPost]
        [SwaggerResponse(201, typeof(V1.User), "The newly registered user.")]
        [SwaggerResponse(400, Description = "No or invalid model data has been provided.")]
        public async Task<IActionResult> RegisterUser([FromBody] V1.Register model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var request = Mapper.Map<RegisterUser>(model);
            await _mediator.Send(request);

            return CreatedAtRoute(
                Routes.GetUserById, 
                new {id = request.Id}, 
                Mapper.Map<V1.User>(request));
        }

        private static class Routes
        {
            public const string GetUserById = nameof(GetUserById);
        }
    }
}