using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Dtos;
using BookStore.Services;


namespace BookStore.Controllers
{
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreClient _client;
        public BooksController(BookStoreClient client) => _client = client;

        [HttpGet]
        public async Task<ActionResult<List<ItBookItem>>> GetBooks(CancellationToken ct)
            => await _client.SearchMysqlAsync(ct);
    }
}
