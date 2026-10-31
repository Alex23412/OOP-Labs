using System;

namespace Лаба_9_ООП
{
    internal class Time
    {
        private static int count = 0;

        private int hours;
        private int minutes;

        public static int Count
        {
            get { return count; }
        }

        public int Hours
        {
            get { return hours; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(
                        "Часы не могут быть отрицательными");
                }

                hours = value;
            }
        }

        public int Minutes
        {
            get { return minutes; }
            set
            {
                if (value < 0 || value > 59)
                {
                    throw new ArgumentException("Минуты должны находиться в диапазоне от 0 до 59");
                }

                minutes = value;
            }
        }

        // Создание объекта времени
        public Time(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;

            count++;
        }

        public override string ToString()
        {
            return $"Время: {Hours:D2}:{Minutes:D2}";
        }

        // Статическое вычитание минут
        public static Time SubMinutes(Time time, int minutes)
        {
            if (time == null)
            {
                throw new ArgumentException("Объект времени не может быть null");
            }

            if (minutes < 0)
            {
                throw new ArgumentException(
                    "Количество минут не может быть отрицательным");
            }

            int totalMinutes = time.Hours * 60 + time.Minutes;
            totalMinutes -= minutes;

            if (totalMinutes < 0)
            {
                throw new ArgumentException(
                    "Результат не может быть отрицательным");
            }

            return new Time(totalMinutes / 60, totalMinutes % 60);
        }

        // Обычный метод вычитания минут
        public Time SubMinutes(int minutes)
        {
            return SubMinutes(this, minutes);
        }

        // Обнуление времени
        public static Time operator -(Time time)
        {
            if (time == null)
            {
                throw new ArgumentException("Объект времени не может быть null");
            }

            return new Time(0, 0);
        }

        // Вычитание одной минуты
        public static Time operator --(Time time)
        {
            return SubMinutes(time, 1);
        }

        // Неявное преобразование в int (количество часов)
        public static implicit operator int(Time time)
        {
            return time.Hours;
        }

        // Явное преобразование в bool
        public static explicit operator bool(Time time)
        {
            return time != null &&
                   (time.Hours != 0 || time.Minutes != 0);
        }

        // Сравнение двух объектов
        public static bool operator ==(Time first, Time second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            return first.Hours == second.Hours &&
                   first.Minutes == second.Minutes;
        }

        public static bool operator !=(Time first, Time second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            return obj is Time other && this == other;
        }

        public override int GetHashCode()
        {
            return Hours * 60 + Minutes;
        }
    }
}