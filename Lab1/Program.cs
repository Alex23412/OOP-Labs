using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        { 
            

            Console.WriteLine("ЗАДАНИЕ 1");

            Task1_1();
            Task1_2();
            Task1_3();
            Task1_4();

            

            Console.WriteLine();
            Console.WriteLine("ЗАДАНИЕ 2");

            double x1;

            while (true)
            {
                Console.Write("Введите X: ");

                if (double.TryParse(Console.ReadLine(), out x1))
                {
                    break;
                }

                Console.WriteLine("Ошибка! Введите число.");
            }

            double y1;

            while (true)
            {
                Console.Write("Введите Y: ");

                if (double.TryParse(Console.ReadLine(), out y1))
                {
                    break;
                }

                Console.WriteLine("Ошибка! Введите число.");
            }

            Task2(x1, y1);


            
            

            Console.WriteLine();
            Console.WriteLine("ЗАДАНИЕ 3");

            Task3();
        }


        
        

        static void Task1_1()
        {
            Console.WriteLine();
            Console.WriteLine("Задание 1.1");

            int m;

            while (true)
            {
                Console.Write("Введите m: ");

                if (!int.TryParse(Console.ReadLine(), out m))
                {
                    Console.WriteLine("Ошибка! Введите целое число.");
                    continue;
                }

                if (m == 1)
                {
                    Console.WriteLine(
                        "Ошибка! При m = 1 вычисление невозможно."
                    );
                    continue;
                }

                break;
            }


            int n;

            while (true)
            {
                Console.Write("Введите n: ");

                if (int.TryParse(Console.ReadLine(), out n))
                {
                    break;
                }

                Console.WriteLine("Ошибка! Введите целое число.");
            }


            int result = n++ / --m;

            result += 1;


            Console.WriteLine("Результат: " + result);
            Console.WriteLine("m: " + m);
            Console.WriteLine("n: " + n);
        }


        
        
        

        static void Task1_2()
        {
            Console.WriteLine();
            Console.WriteLine("Задание 1.2");

            int m;

            while (true)
            {
                Console.Write("Введите m: ");

                if (int.TryParse(Console.ReadLine(), out m))
                {
                    break;
                }

                Console.WriteLine("Ошибка! Введите целое число.");
            }


            int n;

            while (true)
            {
                Console.Write("Введите n: ");

                if (int.TryParse(Console.ReadLine(), out n))
                {
                    break;
                }

                Console.WriteLine("Ошибка! Введите целое число.");
            }


            bool result = ++m < n--;


            Console.WriteLine("Результат: " + result);
            Console.WriteLine("m: " + m);
            Console.WriteLine("n: " + n);
        }


        
        
        

        static void Task1_3()
        {
            Console.WriteLine();
            Console.WriteLine("Задание 1.3");

            int m;

            while (true)
            {
                Console.Write("Введите m: ");

                if (int.TryParse(Console.ReadLine(), out m))
                {
                    break;
                }

                Console.WriteLine("Ошибка! Введите целое число.");
            }


            int n;

            while (true)
            {
                Console.Write("Введите n: ");

                if (int.TryParse(Console.ReadLine(), out n))
                {
                    break;
                }

                Console.WriteLine("Ошибка! Введите целое число.");
            }


            bool result = --m > ++n;


            Console.WriteLine("Результат: " + result);
            Console.WriteLine("m: " + m);
            Console.WriteLine("n: " + n);
        }


        

        static void Task1_4()
        {
            Console.WriteLine();
            Console.WriteLine("Задание 1.4");

            double x;

            while (true)
            {
                Console.Write("Введите x: ");

                if (!double.TryParse(Console.ReadLine(), out x))
                {
                    Console.WriteLine("Ошибка! Введите число.");
                    continue;
                }

                if (x == 0)
                {
                    Console.WriteLine(
                        "Ошибка! x не может быть равен 0."
                    );
                    continue;
                }

                break;
            }


            double innerSum =
                Math.Exp(x) + Math.Tan(x);


            double cubeRoot =
                Math.Pow(
                    innerSum,
                    1.0 / 3.0
                );


            double result =
                cubeRoot + 1.0 / x;


            Console.WriteLine("Результат: " + result);
        }


        
  
        

        static void Task2(double x1, double y1)
        {
            bool result =
                (x1 * x1 + y1 * y1 <= 1)
                &&
                (x1 + y1 >= -1);


            Console.WriteLine(
                "Точка принадлежит заштрихованной области: "
                + result
            );
        }


        
       
        

        static void Task3()
        {
            
            
            

            float aFloat = 1000f;
            float bFloat = 0.0001f;


            float sumFloat =
                aFloat + bFloat;


            float a1Float =
                (float)Math.Pow(
                    sumFloat,
                    4
                );


            float a2Float =
                (float)Math.Pow(
                    aFloat,
                    4
                );


            float a3Float =
                6
                * (float)Math.Pow(
                    aFloat,
                    2
                )
                * (float)Math.Pow(
                    bFloat,
                    2
                );


            float a4Float =
                4
                * aFloat
                * (float)Math.Pow(
                    bFloat,
                    3
                );


            float numeratorFloat =
                a1Float
                -
                (
                    a2Float
                    + a3Float
                    + a4Float
                );


            float denominatorFloat =
                (float)Math.Pow(
                    bFloat,
                    4
                )
                +
                4
                * (float)Math.Pow(
                    aFloat,
                    3
                )
                * bFloat;


            float resultFloat =
                numeratorFloat
                /
                denominatorFloat;


            
    
            

            double aDouble = 1000;
            double bDouble = 0.0001;


            double sumDouble =
                aDouble + bDouble;


            double a1Double =
                Math.Pow(
                    sumDouble,
                    4
                );


            double a2Double =
                Math.Pow(
                    aDouble,
                    4
                );


            double a3Double =
                6
                * Math.Pow(
                    aDouble,
                    2
                )
                * Math.Pow(
                    bDouble,
                    2
                );


            double a4Double =
                4
                * aDouble
                * Math.Pow(
                    bDouble,
                    3
                );


            double numeratorDouble =
                a1Double
                -
                (
                    a2Double
                    + a3Double
                    + a4Double
                );


            double denominatorDouble =
                Math.Pow(
                    bDouble,
                    4
                )
                +
                4
                * Math.Pow(
                    aDouble,
                    3
                )
                * bDouble;


            double resultDouble =
                numeratorDouble
                /
                denominatorDouble;


            
    
            

            Console.WriteLine(
                "Результат float: "
                + resultFloat
            );


            Console.WriteLine(
                "Результат double: "
                + resultDouble
            );
        }
    }
}