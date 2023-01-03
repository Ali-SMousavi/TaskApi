using System.ComponentModel.DataAnnotations;

namespace TaskWebApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
