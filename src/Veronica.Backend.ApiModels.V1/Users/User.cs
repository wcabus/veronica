using System;

namespace Veronica.Backend.ApiModels.V1.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}