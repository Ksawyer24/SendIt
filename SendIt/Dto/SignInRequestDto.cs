using System.ComponentModel.DataAnnotations;

namespace SendIt.Dto
{
    public class SignInRequestDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
