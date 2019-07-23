using Veronica.Backend.Application.Users;

namespace Veronica.Backend.ApiModels.V1.Users
{
    public class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            // From API to Application
            CreateMap<Register, RegisterUser>();

            // From Application to API
            CreateMap<RegisterUser, User>();
            CreateMap<Domain.Models.User, User>();
        }
    }
}