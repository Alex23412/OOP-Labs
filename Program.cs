using System;

class Program
{
    static void Main()
    {
        double a = 0.1;      
        double b = 0.8;      
        int k = 9;            
        int nFixed = 35;      
        double eps = 0.0001;  

        double h = (b - a) / k;

        Console.WriteLine("Вычисление функции");

        for (int i = 0; i <= k; i++)
        {
            double x = a + i * h;

            double sn = SumFixedN(x, nFixed);
            double se = SumByEps(x, eps);
            double y = ExactValue(x);

            
            Console.WriteLine($"X={x:F4}\tSN={sn:F6}\tSE={se:F6}\tY={y:F6}");
        }
    }

    
    static double SumFixedN(double x, int n)
    {
        double sum = 0;
        double c = 1;

        for (int i = 1; i <= n; i++)
        {
            c *= x;                                 
            double term = c * Math.Cos(i * Math.PI / 3) / i; 
            sum += term;
        }
        return sum;
    }

    
    static double SumByEps(double x, double eps)
    {
        double sum = 0;
        double c = 1;
        int i = 0;
        double term;

        do
        {
            i++;
            c *= x;
            term = c * Math.Cos(i * Math.PI / 3) / i;
            sum += term;
        }
        while (Math.Abs(term) >= eps);

        return sum;
    }

    
    static double ExactValue(double x)
    {
        return -0.5 * Math.Log(1 - 2 * x * Math.Cos(Math.PI / 3) + x * x);
    }
}