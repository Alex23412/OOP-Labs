using System;

namespace Лаба_9_ООП
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Часть 1");
            Part1();

            Console.WriteLine("Часть 2");
            Part2();

            Console.WriteLine("Часть 3");
            Part3();

            Console.WriteLine("Количество созданных объектов Time: "
                + Time.Count);

            Console.WriteLine("Количество созданных объектов TimeArray: "
                + TimeArray.Count);
        }

        // Демонстрация методов класса Time
        static void Part1()
        {
            Time time = new Time(14, 10);

            Console.WriteLine(time);

            Time staticResult = Time.SubMinutes(time, 25);
            Time methodResult = time.SubMinutes(25);

            Console.WriteLine("Статический метод: " + staticResult);
            Console.WriteLine("Обычный метод: " + methodResult);

            Console.WriteLine("Исходное время: " + time);
        }

        // Демонстрация перегруженных операций
        static void Part2()
        {
            Time time = new Time(14, 10);

            Time zero = -time;

            Console.WriteLine("Исходное время: " + time);
            Console.WriteLine("Обнуление: " + zero);

            time--;

            Console.WriteLine("После вычитания минуты: " + time);

            try
            {
                Time invalid = new Time(0, 0);
                invalid--;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }

            int hours = time;
            bool isNotZero = (bool)time;

            Console.WriteLine("Количество часов: " + hours);
            Console.WriteLine("Время не равно нулю: " + isNotZero);

            Console.WriteLine(
                "Проверка 00:00: " + (bool)new Time(0, 0));

            Console.WriteLine(
                "Проверка 00:30: " + (bool)new Time(0, 30));

            Time first = new Time(14, 10);
            Time second = new Time(14, 10);
            Time third = new Time(14, 20);

            Console.WriteLine("Первое == второе: " + (first == second));
            Console.WriteLine("Первое == третье: " + (first == third));
            Console.WriteLine("Первое != третье: " + (first != third));
        }

        // Демонстрация класса TimeArray
        static void Part3()
        {
            TimeArray empty = new TimeArray();

            Console.WriteLine("Пустой массив:");
            UI.ShowArray(empty);

            TimeArray randomArray = new TimeArray(5);

            Console.WriteLine("\nСлучайный массив:");
            UI.ShowArray(randomArray);

            UI.ShowMin(FindMin(randomArray));

            Console.WriteLine("\nВвод массива с клавиатуры:");

            TimeArray inputArray = UI.ReadTimeArray();

            UI.ShowArray(inputArray);

            if (inputArray.Length > 0)
            {
                UI.ShowMin(FindMin(inputArray));
            }
            else
            {
                Console.WriteLine("Массив пуст, минимум отсутствует.");
            }

            Console.WriteLine("\nПроверка индексатора:");

            Console.WriteLine("Первый элемент: " + randomArray[0]);

            randomArray[0] = new Time(0, 1);

            Console.WriteLine("После изменения: " + randomArray[0]);

            try
            {
                Time invalid = randomArray[10];
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
        }

        // Поиск минимального времени
        static Time FindMin(TimeArray array)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException(
                    "Массив пуст, минимальное значение отсутствует");
            }

            Time min = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                int currentMinutes =
                    array[i].Hours * 60 + array[i].Minutes;

                int minMinutes =
                    min.Hours * 60 + min.Minutes;

                if (currentMinutes < minMinutes)
                {
                    min = array[i];
                }
            }

            return min;
        }
    }
}