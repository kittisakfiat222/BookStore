namespace BookStore.Dtos;

// Request สำหรับสมัครสมาชิก
public record RegisterRequest(
    string Username,
    string Password,
    string Fullname
);

// Request สำหรับล็อกอิน
public record LoginRequest(
    string Username,
    string Password
);

// Response หลังล็อกอิน/สมัครสำเร็จ
public record AuthResponse(
    string Token,
    int UserId,
    string Username,
    string Fullname
);
