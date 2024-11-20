using System.ComponentModel.DataAnnotations;

namespace SendIt.Dto
{
    public class ConfirmationEmailRequestDto
    {
        public SignUpRequestDto User { get; set; }
        public string Email { get; set; }
        public string ConfirmationLink { get; set; }
    }

    public class PasswordResetCodeRequestDto
    {
        public SignUpRequestDto User { get; set; }
        public string Email { get; set; }
        public string ResetCode { get; set; }
    }

    public class PasswordResetLinkRequestDto
    {
        public SignUpRequestDto User { get; set; }
        public string Email { get; set; }
        public string ResetLink { get; set; }
    }

}
