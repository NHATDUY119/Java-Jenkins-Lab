using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Lắng nghe các yêu cầu gửi đến đường dẫn gốc "/"
app.MapGet("/", (HttpContext context) =>
{
    // Lấy dữ liệu từ URL (khi người dùng bấm nút Tính)
    string? aStr = context.Request.Query["a"];
    string? bStr = context.Request.Query["b"];
    string? op = context.Request.Query["op"];
    
    string resultMessage = "";
    
    // Nếu có dữ liệu gửi lên thì mới tính toán
    if (!string.IsNullOrEmpty(aStr) && !string.IsNullOrEmpty(bStr) && !string.IsNullOrEmpty(op))
    {
        try
        {
            int a = int.Parse(aStr);
            int b = int.Parse(bStr);
            
            // Xử lý các phép toán
            resultMessage = op switch
            {
                "add" => $"Kết quả: {a} + {b} = {a + b}",
                "sub" => $"Kết quả: {a} - {b} = {a - b}",
                "mul" => $"Kết quả: {a} * {b} = {a * b}",
                "div" => b != 0 ? $"Kết quả: {a} / {b} = {(double)a / b}" : "<span style='color:red;'>Lỗi: Không thể chia cho 0.</span>",
                _ => "Phép toán không hợp lệ"
            };
        }
        catch
        {
            resultMessage = "<span style='color:red;'>Lỗi: Vui lòng nhập số hợp lệ.</span>";
        }
    }

    // Tạo mã HTML để trả về cho trình duyệt
    string html = $@"
    <!DOCTYPE html>
    <html lang='vi'>
    <head>
        <meta charset='UTF-8'>
        <title>Máy Tính C# Minimal API</title>
        <style>
            body {{ font-family: Arial, sans-serif; text-align: center; margin-top: 50px; background-color: #f4f4f9; }}
            form {{ background: white; padding: 20px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1); display: inline-block; }}
        </style>
    </head>
    <body>
        <h2>Máy Tính Bằng C# Minimal API</h2>
        <form method='GET'>
            <input type='number' name='a' required placeholder='Số thứ 1'> 
            <select name='op'>
                <option value='add'>Cộng (+)</option>
                <option value='sub'>Trừ (-)</option>
                <option value='mul'>Nhân (*)</option>
                <option value='div'>Chia (/)</option>
            </select> 
            <input type='number' name='b' required placeholder='Số thứ 2'> 
            <button type='submit'>Tính</button>
        </form>
        <h3>{resultMessage}</h3>
    </body>
    </html>";

    // Trả về HTML với mã trạng thái 200 OK
    return Results.Content(html, "text/html");
});

// Chạy máy chủ ở cổng 8081
app.Run("http://localhost:8081");