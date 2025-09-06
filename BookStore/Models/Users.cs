using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml;


namespace BookStore.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required, MaxLength(200)]
        public string FullName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserLikes> Likes { get; set; }

        public static Users Create(string username, string plainPassword, string fullName)
        {
            return new Users
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword),
                FullName = fullName
            };
        }

        public bool VerifyPassword(string plainPassword) =>
            BCrypt.Net.BCrypt.Verify(plainPassword, PasswordHash);
    }



}
