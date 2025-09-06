using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;


namespace BookStore.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestDbController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TestDbController(AppDbContext db) => _db = db;

        [HttpGet("tables")]
        public async Task<IActionResult> GetTables()
        {
            try
            {
                var userCount = await _db.Users.CountAsync();
                var likeCount = await _db.UserLikes.CountAsync();
                return Ok(new { Users = userCount, UserLikes = likeCount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "DB error", error = ex.Message });
            }
        }
    }
}
