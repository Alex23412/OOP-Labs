using System;

namespace Лаба_9_ООП
{
    internal class TimeArray
    {
        private static int count = 0;
        private static Random random = new Random();

        private Time[] arr;

        public static int Count
        {
            get { return count; }
        }

        private Time[] Items
        {
            get { return arr; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "Массив не может быть null");
                }

                arr = value;
            }
        }

        public int Length
        {
            get { return arr.Length; }
        }

        // Создание пустого массива
        public TimeArray()
        {
            Items = new Time[0];
            count++;
        }

        // Создание массива со случайными значениями
        public TimeArray(int size)
        {
            if (size < 0)
            {
                throw new ArgumentException(
                    "Размер массива не может быть отрицательным");
            }

            Time[] temp = new Time[size];

            for (int i = 0; i < size; i++)
            {
                temp[i] = new Time(
                    random.Next(0, 24),
                    random.Next(0, 60));
            }

            Items = temp;
            count++;
        }

        // Создание массива из введённых элементов
        public TimeArray(Time[] items)
        {
            if (items == null)
            {
                throw new ArgumentException(
                    "Массив не может быть null");
            }

            Time[] temp = new Time[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    throw new ArgumentException(
                        "Элемент массива не может быть null");
                }

                temp[i] = items[i];
            }

            Items = temp;
            count++;
        }

        // Индексатор
        public Time this[int index]
        {
            get
            {
                if (index < 0 || index >= arr.Length)
                {
                    throw new ArgumentException(
                        "Индекс выходит за пределы массива");
                }

                return arr[index];
            }

            set
            {
                if (index < 0 || index >= arr.Length)
                {
                    throw new ArgumentException(
                        "Индекс выходит за пределы массива");
                }

                if (value == null)
                {
                    throw new ArgumentException(
                        "Элемент не может быть null");
                }

                arr[index] = value;
            }
        }

        // Просмотр элементов массива
        public string Show()
        {
            if (arr.Length == 0)
            {
                return "Массив пуст";
            }

            string result = "";

            for (int i = 0; i < arr.Length; i++)
            {
                result += $"[{i}] {arr[i]}";

                if (i < arr.Length - 1)
                {
                    result += Environment.NewLine;
                }
            }

            return result;
        }
    }
}