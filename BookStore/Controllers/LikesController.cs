using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Dtos;
using BookStore.Models;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("user")]
    public class LikesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LikesController(AppDbContext db)
        {
            _db = db;
        }

        // POST /user/like
        [Authorize]
        [HttpPost("like")]
        public async Task<IActionResult> Like([FromBody] LikeRequest req)
        {
            // ดึง userId จาก JWT token
            int userIdFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var userId = req.User_Id != 0 ? req.User_Id : userIdFromToken;

            if (userId == 0 || string.IsNullOrWhiteSpace(req.Book_Id))
                return BadRequest(new { error = "user_id and book_id required" });

            // ตรวจสอบว่าผู้ใช้มีอยู่จริง
            if (!await _db.Users.AnyAsync(u => u.Id == userId))
                return NotFound(new { error = "user not found" });

            var like =  new UserLikes
            {
                UserId = userId,
                BookId = req.Book_Id
            };

            _db.UserLikes.Add(like);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Unique constraint: (UserId, BookIdFromApi)
                return Conflict(new { error = "already liked" });
            }

            return Ok(new { message = "liked" });
        }
    }
}
