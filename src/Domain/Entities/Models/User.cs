using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Dotby.Domain.Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int MatricNumber { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? ProfileImageUrl { get; set; } = "https://res.cloudinary.com/dehztkybw/image/upload/v1748899042/IlE-Mi_Images/jvqdjse8akqvbhxv3wju.png";

    }
}