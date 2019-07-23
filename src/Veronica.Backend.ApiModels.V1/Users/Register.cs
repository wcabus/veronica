using System.ComponentModel.DataAnnotations;

namespace Veronica.Backend.ApiModels.V1.Users
{
    public class Register
    {
        [Required, StringLength(255), EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }
    }
}
