package com.calculator; // [cite: 888]

public class App { // [cite: 891]
    public int add(int a, int b) { return a + b; } // [cite: 894]
    public int subtract(int a, int b) { return a - b; } // [cite: 895]
    public int multiply(int a, int b) { return a * b; } // [cite: 897]
    
    public int divide(int a, int b) { // [cite: 900]
        if (b == 0) throw new ArithmeticException("Divide by zero"); // [cite: 911]
        return a / b; // [cite: 911]
    }

    public static void main(String[] args) { // [cite: 912]
        App calc = new App(); // [cite: 913]
        
        System.out.println("May tinh dang hoat dong!"); // [cite: 914]
        System.out.println("==================================="); // [cite: 916]
        System.out.println("KET QUA THUAT TOAN MAY TINH: "); // [cite: 919]
        
        System.out.println("Phep cong (10+5) = " + calc.add(10, 5)); // [cite: 921, 922]
        System.out.println("Phep tru (10-5) = " + calc.subtract(10, 5)); // [cite: 924, 925]
        System.out.println("Phep nhan (10*5) = " + calc.multiply(10, 5)); // [cite: 927, 928]
        System.out.println("Phep chia (10/5) = " + calc.divide(10, 5)); // [cite: 930]
        System.out.println("Tran Nhat Duy đang test");
        System.out.println("==================================="); // [cite: 932]
    }
}