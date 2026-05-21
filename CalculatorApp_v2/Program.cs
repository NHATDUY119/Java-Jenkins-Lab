using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (HttpContext context) =>
{
    // Lấy dữ liệu từ URL
    string? aStr = context.Request.Query["a"];
    string? bStr = context.Request.Query["b"];
    string? op = context.Request.Query["op"];
    
    string resultMessage = "";
    string errorClass = "";
    
    if (!string.IsNullOrEmpty(aStr) && !string.IsNullOrEmpty(bStr) && !string.IsNullOrEmpty(op))
    {
        try
        {
            // Dùng double để hỗ trợ tính cả số thập phân
            double a = double.Parse(aStr); 
            double b = double.Parse(bStr);
            
            resultMessage = op switch
            {
                "add" => $"Kết quả: {a} + {b} = {a + b}",
                "sub" => $"Kết quả: {a} - {b} = {a - b}",
                "mul" => $"Kết quả: {a} * {b} = {a * b}",
                "div" => b != 0 ? $"Kết quả: {a} / {b} = {a / b}" : "Lỗi: Không thể chia cho 0.",
                _ => "Phép toán không hợp lệ"
            };
            
            if (resultMessage.StartsWith("Lỗi")) errorClass = "error";
        }
        catch
        {
            resultMessage = "Lỗi: Vui lòng nhập số hợp lệ.";
            errorClass = "error";
        }
    }

    // Sử dụng cú pháp $$""" """ để nhúng HTML/CSS. 
    // Trong này, C# sẽ dùng {{ }} để gọi biến, còn CSS vẫn giữ nguyên { }
    string html = $$"""
    <!DOCTYPE html>
    <html lang="vi">
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Máy Tính Thông Minh</title>
        <style>
            @import url('https://fonts.googleapis.com/css2?family=Space+Grotesk:wght@400;500;700&display=swap');

            * { box-sizing: border-box; margin: 0; padding: 0; }
            html, body { min-height: 100%; }
            body {
                font-family: 'Space Grotesk', 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                background:
                    radial-gradient(circle at top left, rgba(255, 255, 255, 0.32), transparent 28%),
                    radial-gradient(circle at bottom right, rgba(56, 189, 248, 0.24), transparent 32%),
                    linear-gradient(135deg, #0f172a 0%, #1e293b 48%, #0f766e 100%);
                min-height: 100vh;
                display: flex;
                align-items: center;
                justify-content: center;
                overflow: hidden;
                position: relative;
                padding: 24px;
                color: #e2e8f0;
            }
            body::before,
            body::after {
                content: '';
                position: absolute;
                border-radius: 999px;
                filter: blur(16px);
                opacity: 0.7;
                pointer-events: none;
            }
            body::before {
                width: 240px;
                height: 240px;
                background: rgba(59, 130, 246, 0.28);
                top: -40px;
                left: -60px;
            }
            body::after {
                width: 320px;
                height: 320px;
                background: rgba(45, 212, 191, 0.2);
                right: -120px;
                bottom: -140px;
            }
            .calculator-shell {
                position: relative;
                width: 100%;
                max-width: 460px;
                z-index: 1;
            }
            .calculator-card {
                background: rgba(15, 23, 42, 0.42);
                border: 1px solid rgba(255, 255, 255, 0.22);
                backdrop-filter: blur(22px);
                -webkit-backdrop-filter: blur(22px);
                border-radius: 28px;
                box-shadow: 0 24px 80px rgba(2, 6, 23, 0.48);
                padding: 34px;
                width: 100%;
            }
            .headline {
                text-align: center;
                margin-bottom: 26px;
            }
            .badge {
                display: inline-flex;
                align-items: center;
                gap: 8px;
                padding: 8px 14px;
                margin-bottom: 14px;
                border-radius: 999px;
                background: rgba(255, 255, 255, 0.12);
                border: 1px solid rgba(255, 255, 255, 0.16);
                color: rgba(255, 255, 255, 0.82);
                font-size: 13px;
                letter-spacing: 0.08em;
                text-transform: uppercase;
            }
            h2 {
                color: #f8fafc;
                text-align: center;
                margin-bottom: 10px;
                font-weight: 700;
                font-size: 30px;
                letter-spacing: -0.03em;
            }
            .subtitle {
                color: rgba(226, 232, 240, 0.76);
                text-align: center;
                font-size: 15px;
                line-height: 1.5;
            }
            .input-group { margin-bottom: 20px; }
            label { display: block; margin-bottom: 8px; color: rgba(241, 245, 249, 0.9); font-size: 14px; font-weight: 600; }
            input[type="number"], select {
                width: 100%;
                padding: 15px 16px;
                border: 1px solid rgba(255, 255, 255, 0.18);
                border-radius: 16px;
                font-size: 16px;
                transition: transform 0.2s ease, border-color 0.2s ease, box-shadow 0.2s ease, background 0.2s ease;
                outline: none;
                background: rgba(255, 255, 255, 0.12);
                color: #f8fafc;
                box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.08);
            }
            input[type="number"]:focus, select:focus {
                border-color: rgba(125, 211, 252, 0.8);
                background: rgba(255, 255, 255, 0.16);
                box-shadow: 0 0 0 4px rgba(56, 189, 248, 0.15);
            }
            input[type="number"]::placeholder { color: rgba(226, 232, 240, 0.5); }
            select option { color: #0f172a; }
            button {
                width: 100%;
                background: linear-gradient(135deg, rgba(56, 189, 248, 0.95) 0%, rgba(14, 165, 233, 0.92) 45%, rgba(45, 212, 191, 0.88) 100%);
                color: #eff6ff;
                border: none;
                padding: 16px;
                border-radius: 16px;
                font-size: 18px;
                font-weight: bold;
                cursor: pointer;
                transition: transform 0.2s ease, box-shadow 0.2s ease, filter 0.2s ease;
                margin-top: 10px;
                box-shadow: 0 18px 30px rgba(8, 145, 178, 0.28);
            }
            button:hover {
                transform: translateY(-2px);
                box-shadow: 0 22px 34px rgba(8, 145, 178, 0.34);
                filter: brightness(1.02);
            }
            button:active { transform: translateY(0); }
            .result-box {
                margin-top: 24px;
                padding: 16px 18px;
                border-radius: 18px;
                background: rgba(16, 185, 129, 0.14);
                border: 1px solid rgba(110, 231, 183, 0.28);
                color: #d1fae5;
                font-size: 17px;
                font-weight: 600;
                text-align: center;
                display: {{ (!string.IsNullOrEmpty(resultMessage) ? "block" : "none") }};
                backdrop-filter: blur(14px);
                -webkit-backdrop-filter: blur(14px);
            }
            .result-box.error {
                background: rgba(239, 68, 68, 0.14);
                border-color: rgba(248, 113, 113, 0.32);
                color: #fee2e2;
            }
            .footer-note {
                margin-top: 14px;
                text-align: center;
                font-size: 13px;
                color: rgba(226, 232, 240, 0.64);
            }
            @media (max-width: 480px) {
                body { padding: 16px; }
                .calculator-card { padding: 24px; border-radius: 24px; }
                h2 { font-size: 26px; }
                .subtitle { font-size: 14px; }
                button { font-size: 16px; }
                .result-box { font-size: 16px; }
            }
        </style>
    </head>
    <body>
        <div class="calculator-shell">
        <div class="calculator-card">
            <div class="headline">
                <div class="badge">Glass UI</div>
                <h2>Máy Tính C#</h2>
                <div class="subtitle">Giao diện trong suốt, mượt hơn và dễ nhìn hơn cho các phép tính nhanh.</div>
            </div>
            <form method="GET">
                <div class="input-group">
                    <label>Số thứ nhất</label>
                    <input type="number" step="any" name="a" required placeholder="Ví dụ: 10" value="{{aStr}}"> 
                </div>
                <div class="input-group">
                    <label>Phép toán</label>
                    <select name="op">
                        <option value="add" {{ (op == "add" ? "selected" : "") }}>Cộng (+)</option>
                        <option value="sub" {{ (op == "sub" ? "selected" : "") }}>Trừ (-)</option>
                        <option value="mul" {{ (op == "mul" ? "selected" : "") }}>Nhân (*)</option>
                        <option value="div" {{ (op == "div" ? "selected" : "") }}>Chia (/)</option>
                    </select> 
                </div>
                <div class="input-group">
                    <label>Số thứ hai</label>
                    <input type="number" step="any" name="b" required placeholder="Ví dụ: 5" value="{{bStr}}"> 
                </div>
                <button type="submit">Tính Toán</button>
            </form>
            <div class="result-box {{errorClass}}">{{resultMessage}}</div>
            <div class="footer-note">Nhập 2 số, chọn phép toán, rồi xem kết quả hiển thị theo kiểu kính mờ.</div>
        </div>
        </div>
    </body>
    </html>
    """;

    return Results.Content(html, "text/html");
});

app.Run("http://localhost:8081");