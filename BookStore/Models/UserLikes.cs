using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Xml;

namespace BookStore.Models
{
    public class UserLikes
    {
       
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string BookId { get; set; } // book_id จาก API ภายนอกเป็น string

        [ForeignKey("UserId")]
        public Users User { get; set; }

        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
        
    }
}
