using System;
using System.Text;

namespace Lab5_6
{
    class Program
    {
        static Random rnd = new Random();

        // Тестовый массив строк 
        static readonly string[] TestStrings =
        {
            "В лесу родилась елочка! В лесу она росла. Зимой и летом стройная, зеленая была!",
            "Мама мыла раму;  папа   читал газету. Сегодня хорошая погода, светит солнце!",
            "Привет! Как дела? Всё хорошо, спасибо."
        };

        // =================================================================
        //  Оформление и ввод
        // =================================================================

        // Функция с переменным числом параметров
        static int Max(params int[] values)
        {
            int max = values[0];
            foreach (int v in values)
                if (v > max) max = v;
            return max;
        }

        // params: заголовок в рамке из любого числа строк
        static void Title(params string[] lines)
        {
            int[] lens = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++) lens[i] = lines[i].Length;
            int width = Max(lens) + 4;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔" + new string('═', width) + "╗");
            foreach (string line in lines)
                Console.WriteLine("║  " + line.PadRight(width - 2) + "║");
            Console.WriteLine("╚" + new string('═', width) + "╝");
            Console.ResetColor();
        }

        // Параметр по умолчанию (цвет)
        static void Message(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void Error(string text)
        {
            Message("  [Ошибка] " + text, ConsoleColor.Red);
        }

        // Рекурсивная функция,при неверном вводе вызывает сама себя.
        // Параметры по умолчанию задают допустимый диапазон.
        static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            Console.Write(prompt);
            try
            {
                int value = int.Parse(Console.ReadLine());
                if (value >= min && value <= max)
                    return value;

                string from = min == int.MinValue ? "-∞" : min.ToString();
                string to = max == int.MaxValue ? "+∞" : max.ToString();
                Error($"введите число от {from} до {to}.");
            }
            catch (FormatException)
            {
                Error("нужно ввести целое число (буквы и символы недопустимы).");
            }
            catch (OverflowException)
            {
                Error("число слишком большое или слишком маленькое.");
            }
            catch (ArgumentNullException)
            {
                Error("пустой ввод.");
            }
            return ReadInt(prompt, min, max); // рекурсия
        }

        // Меню 0 - выход/назад
        static int Menu(string title, string exitText, params string[] items)
        {
            Console.WriteLine();
            Title(title);
            for (int i = 0; i < items.Length; i++)
                Console.WriteLine($"  {i + 1}. {items[i]}");
            Console.WriteLine($"  0. {exitText}");
            return ReadInt("Ваш выбор: ", 0, items.Length);
        }

        // =================================================================
        //  Двумерный массив
        // =================================================================

        // Перегрузка CreateMatrix: ручной ввод
        static int[,] CreateMatrix(int rows, int cols)
        {
            int[,] a = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    a[i, j] = ReadInt($"  a[{i + 1},{j + 1}] = ");
            return a;
        }

        // Перегрузка CreateMatrix: датчик случайных чисел
        static int[,] CreateMatrix(int rows, int cols, int min, int max = 9)
        {
            int[,] a = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    a[i, j] = rnd.Next(min, max + 1);
            return a;
        }

        static bool IsEmpty(int[,] a)
        {
            return a == null || a.Length == 0;
        }

        static void CheckNotEmpty(int[,] a)
        {
            if (IsEmpty(a))
                throw new InvalidOperationException("массив пуст, удалять столбец нельзя.");
        }

        // Задание 1: удалить столбец с номером K
        static int[,] DeleteColumn(int[,] a, int k)
        {
            CheckNotEmpty(a);
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);
            if (k < 1 || k > cols)
                throw new IndexOutOfRangeException($"столбца с номером {k} нет (в массиве столбцов: {cols}).");

            int[,] res = new int[rows, cols - 1];
            for (int i = 0; i < rows; i++)
            {
                int c = 0;
                for (int j = 0; j < cols; j++)
                {
                    if (j == k - 1) continue;
                    res[i, c++] = a[i, j];
                }
            }
            return res;
        }

        // Перегрузка Print для двумерного массива
        static void Print(int[,] a, string title = "Массив:")
        {
            Message(title, ConsoleColor.Green);
            if (IsEmpty(a))
            {
                Message("  Массив пуст.", ConsoleColor.Yellow);
                return;
            }
            for (int i = 0; i < a.GetLength(0); i++)
            {
                Console.Write("  ");
                for (int j = 0; j < a.GetLength(1); j++)
                    Console.Write($"{a[i, j],5}");
                Console.WriteLine();
            }
        }

        // =================================================================
        //  Рваный массив
        // =================================================================

        static int[] CreateRow(int number, bool manual)
        {
            int[] row;
            if (manual)
            {
                int len = ReadInt($"  Длина строки {number}: ", 1, 100);
                row = new int[len];
                for (int j = 0; j < len; j++)
                    row[j] = ReadInt($"    a[{number},{j + 1}] = ");
            }
            else
            {
                row = new int[rnd.Next(1, 6)];
                for (int j = 0; j < row.Length; j++)
                    row[j] = rnd.Next(-9, 10);
            }
            return row;
        }

        static int[][] CreateJagged(int rows, bool manual)
        {
            int[][] a = new int[rows][];
            for (int i = 0; i < rows; i++)
                a[i] = CreateRow(i + 1, manual);
            return a;
        }

        static bool IsEmpty(int[][] a)
        {
            return a == null || a.Length == 0;
        }

        // Задание 2 добавить K строк в конец массива.
        // ref - массив меняет размер; manual по умолчанию false (случайное заполнение)
        static void AddRowsToEnd(ref int[][] a, int k, bool manual = false)
        {
            if (IsEmpty(a))
                throw new InvalidOperationException("массив пуст, сначала создайте его.");
            if (k <= 0)
                throw new ArgumentException("количество добавляемых строк должно быть положительным.");

            int oldLen = a.Length;
            Array.Resize(ref a, oldLen + k);
            for (int i = oldLen; i < a.Length; i++)
                a[i] = CreateRow(i + 1, manual);
        }

        // Перегрузка Print для рваного массива
        static void Print(int[][] a, string title = "Рваный массив:")
        {
            Message(title, ConsoleColor.Green);
            if (IsEmpty(a))
            {
                Message("  Массив пуст.", ConsoleColor.Yellow);
                return;
            }
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write($"  [{i + 1}]: ");
                foreach (int x in a[i]) Console.Write($"{x,5}");
                Console.WriteLine();
            }
        }

        // =================================================================
        //  Строки
        // =================================================================

        static bool IsEmpty(string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        // Перегрузка Print для строки
        static void Print(string s, string title = "Строка:")
        {
            Message(title, ConsoleColor.Green);
            if (IsEmpty(s))
                Message("  Строка пуста.", ConsoleColor.Yellow);
            else
                Console.WriteLine("  " + s);
        }

        // Задание 3 удалить первое и последнее слово в каждом предложении
        static string RemoveFirstAndLastWords(string text)
        {
            if (IsEmpty(text))
                throw new InvalidOperationException("строка пуста, обрабатывать нечего.");

            char[] marks = { '.', '!', '?' };
            StringBuilder result = new StringBuilder();
            int start = 0;

            while (start < text.Length)
            {
                // Ищем конец предложения
                int end = text.IndexOfAny(marks, start);
                string sentence;
                string mark;
                if (end == -1)
                {
                    sentence = text.Substring(start);
                    mark = "";
                    start = text.Length;
                }
                else
                {
                    sentence = text.Substring(start, end - start);
                    mark = text[end].ToString();
                    start = end + 1;
                }

                // Слов может быть несколько пробелов подряд пустые мимо
                string[] words = sentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length <= 2) continue; // после удаления ничего не остаётся

                // Слова со 2-го по предпоследнее
                string[] middle = new string[words.Length - 2];
                Array.Copy(words, 1, middle, 0, middle.Length);

                // Знаки внутри не трогаем; убираем только , ; : перед знаком конца предложения
                string part = string.Join(" ", middle).TrimEnd(',', ';', ':');
                if (part.Length == 0) continue;

                part = char.ToUpper(part[0]) + part.Substring(1);

                if (result.Length > 0) result.Append(' ');
                result.Append(part).Append(mark);
            }
            return result.ToString();
        }

        // =================================================================
        //  Меню
        // =================================================================

        static void MatrixMenu(ref int[,] matrix)
        {
            int choice;
            do
            {
                choice = Menu("ДВУМЕРНЫЙ МАССИВ", "Назад в главное меню",
                    "Создать массив вручную",
                    "Создать массив случайными числами",
                    "Вывести массив",
                    "Удалить столбец с номером K");
                try
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                int rows = ReadInt("Количество строк: ", 1, 20);
                                int cols = ReadInt("Количество столбцов: ", 1, 20);
                                matrix = CreateMatrix(rows, cols);
                                Print(matrix, "Созданный массив:");
                                break;
                            }
                        case 2:
                            {
                                int rows = ReadInt("Количество строк: ", 1, 20);
                                int cols = ReadInt("Количество столбцов: ", 1, 20);
                                matrix = CreateMatrix(rows, cols, -9);
                                Print(matrix, "Созданный массив:");
                                break;
                            }
                        case 3:
                            Print(matrix, "Текущий массив:");
                            break;
                        case 4:
                            {
                                CheckNotEmpty(matrix);
                                Print(matrix, "Исходный массив:");
                                int k = ReadInt("Номер удаляемого столбца K: ");
                                matrix = DeleteColumn(matrix, k);
                                Print(matrix, "Результат:");
                                break;
                            }
                    }
                }
                catch (InvalidOperationException ex) { Error(ex.Message); }
                catch (IndexOutOfRangeException ex) { Error(ex.Message); }
                catch (ArgumentException ex) { Error(ex.Message); }
            } while (choice != 0);
        }

        static void JaggedMenu(ref int[][] jagged)
        {
            int choice;
            do
            {
                choice = Menu("РВАНЫЙ МАССИВ", "Назад в главное меню",
                    "Создать массив вручную",
                    "Создать массив случайными числами",
                    "Вывести массив",
                    "Добавить K строк в конец массива");
                try
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                int rows = ReadInt("Количество строк: ", 1, 20);
                                jagged = CreateJagged(rows, true);
                                Print(jagged, "Созданный массив:");
                                break;
                            }
                        case 2:
                            {
                                int rows = ReadInt("Количество строк: ", 1, 20);
                                jagged = CreateJagged(rows, false);
                                Print(jagged, "Созданный массив:");
                                break;
                            }
                        case 3:
                            Print(jagged, "Текущий массив:");
                            break;
                        case 4:
                            {
                                if (IsEmpty(jagged))
                                    throw new InvalidOperationException("массив пуст, сначала создайте его.");
                                Print(jagged, "Исходный массив:");
                                int k = ReadInt("Сколько строк добавить (K): ", 1, 20);
                                int way = ReadInt("Заполнить новые строки (1 - вручную, 2 - случайно): ", 1, 2);
                                if (way == 1) AddRowsToEnd(ref jagged, k, true);
                                else AddRowsToEnd(ref jagged, k); // manual по умолчанию
                                Print(jagged, "Результат:");
                                break;
                            }
                    }
                }
                catch (InvalidOperationException ex) { Error(ex.Message); }
                catch (ArgumentException ex) { Error(ex.Message); }
            } while (choice != 0);
        }

        static void StringMenu(ref string line)
        {
            int choice;
            do
            {
                choice = Menu("ОБРАБОТКА СТРОК", "Назад в главное меню",
                    "Ввести строку с клавиатуры",
                    "Выбрать строку из тестового массива",
                    "Вывести строку",
                    "Удалить первое и последнее слово в каждом предложении");
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Введите строку:");
                            line = Console.ReadLine();
                            Print(line, "Введена строка:");
                            break;
                        case 2:
                            {
                                Message("Тестовые строки:", ConsoleColor.Green);
                                for (int i = 0; i < TestStrings.Length; i++)
                                    Console.WriteLine($"  {i + 1}. {TestStrings[i]}");
                                int n = ReadInt("Номер строки: ", 1, TestStrings.Length);
                                line = TestStrings[n - 1];
                                Print(line, "Выбрана строка:");
                                break;
                            }
                        case 3:
                            Print(line, "Текущая строка:");
                            break;
                        case 4:
                            {
                                string res = RemoveFirstAndLastWords(line);
                                Print(line, "Исходная строка:");
                                if (res.Length == 0)
                                    Message("Результат:\n  Строка пуста (в предложениях слишком мало слов).",
                                            ConsoleColor.Yellow);
                                else
                                    Print(res, "Результат:");
                                break;
                            }
                    }
                }
                catch (InvalidOperationException ex) { Error(ex.Message); }
            } while (choice != 0);
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            int[,] matrix = null;
            int[][] jagged = null;
            string line = "";

            int choice;
            do
            {
                choice = Menu("ПРАКТИЧЕСКАЯ РАБОТА №5-6", "Выход",
                    "Двумерный массив",
                    "Рваный массив",
                    "Обработка строк");

                switch (choice)
                {
                    case 1: MatrixMenu(ref matrix); break;
                    case 2: JaggedMenu(ref jagged); break;
                    case 3: StringMenu(ref line); break;
                    case 0: Message("Работа программы завершена.", ConsoleColor.Cyan); break;
                }
            } while (choice != 0);
        }
    }
}