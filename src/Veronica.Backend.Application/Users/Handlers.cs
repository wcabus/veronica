using System.Threading.Tasks;
using MediatR;
using Veronica.Backend.Domain.Models;
using Veronica.Backend.Domain.Repositories.Users;
using System.Collections.Generic;

namespace Veronica.Backend.Application.Users
{
    public class Handlers : 
        IAsyncRequestHandler<RegisterUser>,
        IAsyncRequestHandler<GetUserById, User>,
        IAsyncRequestHandler<GetUsers, IEnumerable<User>>
    {
        private readonly ICreateUser _createUser;
        private readonly IReadUser _readUser;

        public Handlers(ICreateUser createUser, IReadUser readUser)
        {
            _createUser = createUser;
            _readUser = readUser;
        }

        public Task Handle(RegisterUser message)
        {
            var user = _createUser.CreateUser(message.Id, message.Name, message.Email);
            message.RegistrationDate = user.RegistrationDate;

            return Task.CompletedTask;
        }

        public async Task<User> Handle(GetUserById message)
        {
            return await _readUser.ById(message.Id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> Handle(GetUsers message)
        {
            return await _readUser.All().ConfigureAwait(false);
        }
    }
}