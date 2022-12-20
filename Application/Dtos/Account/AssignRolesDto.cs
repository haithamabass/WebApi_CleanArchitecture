using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Account
{
    public class AssignRolesDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}