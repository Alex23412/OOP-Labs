using System;

namespace Лаба_9_ООП
{
    internal class UI
    {
        // Ввод одного времени
        public static Time ReadTime()
        {
            while (true)
            {
                try
                {
                    Console.Write("Введите часы: ");
                    int hours = int.Parse(Console.ReadLine());

                    Console.Write("Введите минуты: ");
                    int minutes = int.Parse(Console.ReadLine());

                    return new Time(hours, minutes);
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "Ошибка! Необходимо ввести целое число.");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // Ввод размера массива
        public static int ReadSize()
        {
            while (true)
            {
                Console.Write("Введите количество элементов: ");

                if (int.TryParse(Console.ReadLine(), out int size)
                    && size >= 0)
                {
                    return size;
                }

                Console.WriteLine(
                    "Ошибка! Размер должен быть целым числом не меньше 0.");
            }
        }

        // Ввод массива с клавиатуры
        public static TimeArray ReadTimeArray()
        {
            int size = ReadSize();
            Time[] items = new Time[size];

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Элемент [{i}]:");
                items[i] = ReadTime();
            }

            return new TimeArray(items);
        }

        // Вывод массива
        public static void ShowArray(TimeArray array)
        {
            Console.WriteLine(array.Show());
        }

        // Вывод минимального времени
        public static void ShowMin(Time min)
        {
            Console.WriteLine($"Минимальное время: {min}");
        }
    }
}