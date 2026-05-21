package com.calculator; // [cite: 249]

import static org.junit.Assert.assertEquals; // [cite: 252]
import org.junit.Test; // [cite: 254]

public class AppTest { // [cite: 260]
    
    App calc = new App(); // [cite: 263]

    @Test // [cite: 270]
    public void testAdd() { // [cite: 273]
        assertEquals(15, calc.add(10, 5)); // [cite: 275]
    }

    @Test // [cite: 279]
    public void testSubtract() { // [cite: 281]
        assertEquals(5, calc.subtract(10, 5)); // [cite: 283]
    }

    @Test 
    public void testMultiply() { 
        assertEquals(50, calc.multiply(10, 5)); 
    }

    @Test 
    public void testDivide() { 
        assertEquals(2, calc.divide(10, 5)); 
    }
}