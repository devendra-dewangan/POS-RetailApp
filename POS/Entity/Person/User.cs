using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace POS.Entity.Person
{
    public class User : IdentityUser
    {
        public bool IsActive { get; set; } = true;
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
        public int PersonId { get; set; }
        public Employee Employee { get; set; }
    }
}
