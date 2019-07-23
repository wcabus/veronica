using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Veronica.Backend.Domain.Models;
using Veronica.Backend.Domain.Repositories.Dishes;
using Veronica.Backend.Domain.Repositories.Users;

namespace Veronica.Backend.Application.Dishes
{
    public class Handlers : 
        IAsyncRequestHandler<GetDishesForUser, IEnumerable<Dish>>,
        IAsyncRequestHandler<GetDish, Dish>,
        IAsyncRequestHandler<GetDishForUser, Dish>,
        IAsyncRequestHandler<CreateDish>,
        IAsyncRequestHandler<RemoveDish>
    {
        private readonly IVerifyUserExists _verifyUserExists;
        private readonly IVerifyDishExists _verifyDishExists;
        private readonly IReadDish _readDish;
        private readonly ICreateDish _createDish;
        private readonly IRemoveDish _removeDish;

        public Handlers(IVerifyUserExists verifyUserExists, IVerifyDishExists verifyDishExists, IReadDish readDish, ICreateDish createDish, IRemoveDish removeDish)
        {
            _verifyUserExists = verifyUserExists;
            _verifyDishExists = verifyDishExists;
            _readDish = readDish;
            _createDish = createDish;
            _removeDish = removeDish;
        }

        public async Task<IEnumerable<Dish>> Handle(GetDishesForUser message)
        {
            var userExists = await _verifyUserExists.Exists(message.UserId).ConfigureAwait(false);
            if (!userExists)
            {
                return null;
            }

            return await _readDish.AllForUser(message.UserId).ConfigureAwait(false);
        }

        public async Task<Dish> Handle(GetDish message)
        {
            return await _readDish.ById(message.DishId).ConfigureAwait(false);
        }

        public async Task<Dish> Handle(GetDishForUser message)
        {
            var userExists = await _verifyUserExists.Exists(message.UserId).ConfigureAwait(false);
            if (!userExists)
            {
                return null;
            }

            return await _readDish.ById(message.DishId).ConfigureAwait(false);
        }

        public Task Handle(CreateDish message)
        {
            var dish = _createDish.CreateDish(message.Id, message.UserId, message.Name, message.Score, message.LastInMenu);
            message.Added = dish.Added;

            return Task.CompletedTask;
        }

        public async Task Handle(RemoveDish message)
        {
            var dishExists = await _verifyDishExists.Exists(message.UserId, message.DishId).ConfigureAwait(false);
            if (!dishExists)
            {
                message.Succeeded = false;
                return;
            }

            await _removeDish.Remove(message.DishId).ConfigureAwait(false);
            message.Succeeded = true;
        }
    }
}