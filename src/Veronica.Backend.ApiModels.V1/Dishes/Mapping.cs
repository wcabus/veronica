namespace Veronica.Backend.ApiModels.V1.Dishes
{
    public class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            // From API to Application
            CreateMap<CreateDish, Application.Dishes.CreateDish>();

            // From Application to API
            CreateMap<Domain.Models.Dish, Dish>();
            CreateMap<Application.Dishes.CreateDish, Dish>();
        }
    }
}