# BookStore API

A simple **BookStore Web API** built with **ASP.NET Core 8**, **Entity Framework Core**, and **JWT Authentication**.  
Includes authentication, Swagger UI for testing, and an endpoint to like a book.

---

## 🚀 Features

- JWT Authentication (Bearer Token)
- Swagger UI for API testing
- Secure endpoints with `[Authorize]`
- Example endpoint: `/user/like`
- SQL Server database via Entity Framework Core
- HttpClient for external API requests (`https://api.itbook.store`)

---

## 📦 Requirements

- .NET 8 SDK  
- SQL Server  
- Visual Studio 2022 / VS Code  

---

## ⚙️ Setup & Installation

1. **Clone the repo**
   ```bash
   git clone <your-repo-url>
   cd BookStore.Api
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Update appsettings.json**
   ```json
   {
     "ConnectionStrings": {
       "DbConnection": "Server=YOUR_SERVER;Database=BookStoreDb;Trusted_Connection=True;"
     },
     "Jwt": {
       "Key": "115a8d7af5e21937f2e68c1616248086c77f13fd8902d6bd7cfdf1343e7678e18f895b92",
       "Issuer": "bookstore",
       "Audience": "bookstore_api"
     }
   }
   ```

4. **Apply migrations and create database**
   ```bash
   dotnet ef database update
   ```

---

## ▶️ Run the API

```bash
dotnet run
```

Swagger UI will be available at:

```
https://localhost:5001/swagger/index.html
```

---

## 🔑 Authorization (JWT)

1. Generate a JWT token (via `/user/login` or `JwtService`)  
2. In Swagger UI, click the **Authorize** button (top-right)  
3. Enter your token:
   ```
   Bearer {your_jwt_token}
   ```
4. Now you can test secured endpoints like `/user/like`

---

## 📖 Example: Like a Book

**Request**
```http
POST /user/like
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "User_Id": 1,
  "Book_Id": "9781234567890"
}
```

**Response**
```json
{
  "message": "liked"
}
```

---

## ⚠️ Notes

- Do **not** expose your real JWT Key in production Swagger descriptions  
- The `/user/like` endpoint requires a valid token  
- The token is always passed in the `Authorization` header  

---

## 📄 License

MIT License

Copyright (c) 2025 Kittisak Panthong

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

