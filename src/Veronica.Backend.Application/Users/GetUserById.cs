using System;
using Veronica.Backend.Domain.Models;

namespace Veronica.Backend.Application.Users
{
    public class GetUserById : IQuery<User>
    {
        public GetUserById(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}