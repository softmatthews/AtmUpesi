using Domain.User.Group;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers.User
{
    public class ActiveATMDto
    {
        public string ActiveATM { get; set; } = null!;
    }
    public class LoginDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? OtpCode { get; set; }
    }
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        // [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password must be complex")]
        public string Password { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
    }
    public class ResetPasswordDto
    {
        public string NewPassword { get; set; } = null!;
    }
    public class UserDto
    {
        public string DisplayName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool OtpAuthentication { get; set; }
        public bool GoogleAuthenticator { get; set; }
        public string? QrCodeImage { get; set; }
        public string? Image { get; set; }
        public int MinutesRemaining { get; set; }
    }

    public class UserListDto
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public bool OTPAuthentication { get; set; }
        public bool GoogleAuthenticator { get; set; }
        public int? GroupsCount { get; set; }
        public List<Group>? Groups { get; set; }
    }
}