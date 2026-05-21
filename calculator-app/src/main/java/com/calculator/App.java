package com.calculator;

import com.sun.net.httpserver.HttpServer;
import com.sun.net.httpserver.HttpHandler;
import com.sun.net.httpserver.HttpExchange;

import java.io.IOException;
import java.io.OutputStream;
import java.net.InetSocketAddress;
import java.util.HashMap;
import java.util.Map;

public class App {
    // 1. Giữ nguyên các hàm thuật toán cũ
    public int add(int a, int b) { return a + b; }
    public int subtract(int a, int b) { return a - b; }
    public int multiply(int a, int b) { return a * b; }
    public int divide(int a, int b) {
        if (b == 0) throw new ArithmeticException("Divide by zero");
        return a / b;
    }

    // 2. Khởi tạo Web Server
    public static void main(String[] args) throws IOException {
        // Mở cổng 8081
        HttpServer server = HttpServer.create(new InetSocketAddress(8081), 0);
        server.createContext("/", new CalculatorHandler());
        server.setExecutor(null);
        server.start();
        System.out.println("May chu tinh toan dang chay tai cong 8081...");
    }

    // 3. Xử lý yêu cầu từ trình duyệt và trả về HTML
    static class CalculatorHandler implements HttpHandler {
        @Override
        public void handle(HttpExchange t) throws IOException {
            App calc = new App();
            String query = t.getRequestURI().getQuery();
            String result = "";

            // Xử lý khi người dùng bấm nút tính toán
            if (query != null) {
                Map<String, String> params = queryToMap(query);
                try {
                    int a = Integer.parseInt(params.get("a"));
                    int b = Integer.parseInt(params.get("b"));
                    String op = params.get("op");

                    if ("add".equals(op)) result = "Kết quả: " + a + " + " + b + " = " + calc.add(a, b);
                    else if ("sub".equals(op)) result = "Kết quả: " + a + " - " + b + " = " + calc.subtract(a, b);
                    else if ("mul".equals(op)) result = "Kết quả: " + a + " * " + b + " = " + calc.multiply(a, b);
                    else if ("div".equals(op)) result = "Kết quả: " + a + " / " + b + " = " + calc.divide(a, b);
                } catch (Exception e) {
                    result = "<span style='color:red;'>Lỗi: Vui lòng nhập số hợp lệ hoặc không chia cho 0.</span>";
                }
            }

            // Giao diện HTML
            String response = "<!DOCTYPE html><html lang='vi'><head><meta charset='UTF-8'><title>Máy Tính CI/CD</title>"
                    + "<style>body{font-family: Arial, sans-serif; text-align: center; margin-top: 50px; background-color: #f4f4f9;}"
                    + "form{background: white; padding: 20px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1); display: inline-block;}</style></head>"
                    + "<body><h2>Máy Tính Bằng Java</h2>"
                    + "<form method='GET'>"
                    + "<input type='number' name='a' required placeholder='Số thứ 1'> "
                    + "<select name='op'>"
                    + "<option value='add'>Cộng (+)</option>"
                    + "<option value='sub'>Trừ (-)</option>"
                    + "<option value='mul'>Nhân (*)</option>"
                    + "<option value='div'>Chia (/)</option>"
                    + "</select> "
                    + "<input type='number' name='b' required placeholder='Số thứ 2'> "
                    + "<button type='submit'>Tính</button>"
                    + "</form>"
                    + "<h3>" + result + "</h3>"
                    + "</body></html>";

            // Gửi dữ liệu trả về cho trình duyệt
            t.getResponseHeaders().set("Content-Type", "text/html; charset=UTF-8");
            t.sendResponseHeaders(200, response.getBytes("UTF-8").length);
            OutputStream os = t.getResponseBody();
            os.write(response.getBytes("UTF-8"));
            os.close();
        }
    }

    // Hàm phụ trợ để bóc tách tham số URL
    public static Map<String, String> queryToMap(String query) {
        Map<String, String> result = new HashMap<>();
        for (String param : query.split("&")) {
            String[] entry = param.split("=");
            if (entry.length > 1) result.put(entry[0], entry[1]);
        }
        return result;
    }
}