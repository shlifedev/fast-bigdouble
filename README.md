# Fast-BigDouble
 
This source code is a fork of [BreakInfinity](https://github.com/Razenpok/BreakInfinity.cs) that has been modified for idle games that use very large numbers.


## Why Fast?
The functions see in the picture were called 1000 times in one frame

<img width="820" alt="image" src="https://github.com/shlifedev/FastBigDouble/assets/49047211/3623a23a-961d-435a-a555-e6f618d227a3">
 

While **double.Parse** and **double.ToString** are great for their versatility, they are very slow in special situations.

In games that use large numbers, such as idle games, you don't need to care as much about the accuracy of floating-point numbers, so it's a performance advantage to create your own algorithm to parse double.

And Unless it's a case like ToString, which requires a string to be created anew, it will behave as **memory efficient** code with near **zero GC**

## How to Use?
It is exactly the same as BigInfinity.cs, but it must follow these rules

Simple.
```cs
FastBigDouble _ = new BigDouble("1000"); // Number Constructor
FastBigDouble _ = new BigDouble("1.0A"); // Alphabet Constructor
FastBigDouble _ = new BigDouble("999.9A"); // Alphabet Constructor
FastBigDouble _ = new BigDouble("1000A"); // Alphabet Constructur, But It's Throw Error. Alphabet Number Allow -999.9~999.9 for performance.
FastBigDouble _ = new BigDouble("9.999e100"); // Exponent Constructor. It's Very Fast!!!!
FastBigDouble _ = new BigDouble("100e100"); // Exponent Constructor.

new FastBigDouble(1e3).ToString() // Result = "1.0A"
```


