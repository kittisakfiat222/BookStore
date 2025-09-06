namespace BookStore.Dtos;

    // Response ของ itbook.store (ตัดมาเฉพาะที่ใช้)
public record ItBookItem(
    string title,
    string subtitle,
    string isbn13,
    string price,
    string image,
    string url
);

public record ItBookSearchResult(
    int total,
    int page,
    List<ItBookItem> books
);

// Request สำหรับ like หนังสือ
public record LikeRequest(
    int User_Id,
    string Book_Id
);

