using System.ComponentModel.DataAnnotations;

namespace Assignment4.Core
{
    public record UserDTO(int Id, string Name, string Email);
    public record UserCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name {get; init; }

        [EmailAddress]
        [Required]
        [StringLength(100)]
        public string Email {get; init; }
    }

    public record UserUpdateDTO : UserCreateDTO
    {
        public int Id { get; init; }
    }

    //public record UserCreateDTO([Required, StringLength(50)] string Name, [EmailAddress, Required, StringLength(50)] string Email);
    //public record UserDTO(int Id, [Required, StringLength(50)] string Name, [EmailAddress, Required, StringLength(50)] string Email) : UserCreateDTO(Name, Email);
}