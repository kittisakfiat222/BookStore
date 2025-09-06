
using BookStore.Data;
using BookStore.Dtos;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ItBookShop.Api.Controllers;

[ApiController]
[Route("")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly JwtService _jwt;

    public AuthController(AppDbContext db, JwtService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    // POST /register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest("username/password is required");

        var exists = await _db.Users.AnyAsync(u => u.Username == req.Username);
        if (exists) return Conflict("username already exists");

        var user = Users.Create(req.Username, req.Password, req.Fullname ?? "");
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return Created("", new { user.Id, user.Username, user.FullName });
    }

    // POST /login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == req.Username);
        if (user is null || !user.VerifyPassword(req.Password))
            return Unauthorized("invalid credentials");

        var token = _jwt.GenerateToken(user.Id, user.Username, user.FullName);
        return Ok(new AuthResponse(token, user.Id, user.Username, user.FullName));
    }
}