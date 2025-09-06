using BookStore.Dtos;

namespace BookStore.Services
{
    public class BookStoreClient
    {
        private readonly HttpClient _http;

        public BookStoreClient(HttpClient http) => _http = http;

        // ดึงหนังสือจาก API itbook.store
        public async Task<List<ItBookItem>> SearchMysqlAsync(CancellationToken ct = default)
        {
            var result = await _http.GetFromJsonAsync<ItBookSearchResult>(
                "https://api.itbook.store/1.0/search/mysql", ct);

            var books = result?.books ?? new List<ItBookItem>();

            // sort A→Z ตาม title (ignore case)
            return books.OrderBy(b => b.title, StringComparer.OrdinalIgnoreCase).ToList();
        }
    }
}
