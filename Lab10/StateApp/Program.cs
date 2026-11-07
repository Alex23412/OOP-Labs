using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using StateLibrary;

namespace StateApp;

internal class Program
{
    private static readonly Type[] stateTypes = { typeof(State), typeof(Republic), typeof(Monarchy), typeof(Kingdom) };
    private static readonly string[] stateTypeNames = { "Государство", "Республика", "Монархия", "Королевство" };

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        State[] states = CreateRandomStates();
        bool isRunning = true;
        while (isRunning)
        {
            PrintMenu();
            int choice = ReadInt("Ваш выбор: ", 0, 7);
            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    DemonstrateVirtual(states);
                    break;
                case 2:
                    DemonstrateInit();
                    break;
                case 3:
                    RunQueries(states);
                    break;
                case 4:
                    DemonstrateSortByName(states);
                    break;
                case 5:
                    DemonstrateSortByPopulation(states);
                    break;
                case 6:
                    DemonstrateIInit();
                    break;
                case 7:
                    DemonstrateCloning();
                    break;
                default:
                    isRunning = false;
                    break;
            }
            Console.WriteLine();
        }
    }

    // ---------- Создание данных ----------

    // В массиве по 3 объекта каждого типа иерархии, все созданы датчиком случайных чисел
    private static State[] CreateRandomStates()
    {
        var states = new List<State>();
        for (int i = 0; i < 3; i++)
        {
            foreach (Type type in stateTypes)
            {
                State state = (State)Activator.CreateInstance(type);
                state.RandomInit();
                states.Add(state);
            }
        }
        return states.ToArray();
    }

    // ---------- Часть 1 ----------

    private static void DemonstrateVirtual(State[] states)
    {
        Console.WriteLine("Виртуальный метод Show():");
        ShowStates(states);

        Console.WriteLine();
        Console.WriteLine("Обычный метод ShowShortInfo() (вызов через ссылку типа State):");
        foreach (State state in states)
        {
            state.ShowShortInfo();
        }

        Console.WriteLine();
        Console.WriteLine("Show() виртуальный: версия выбирается во время выполнения по типу объекта.");
        Console.WriteLine("ShowShortInfo() обычный: версия выбирается при компиляции по типу ссылки,");
        Console.WriteLine("поэтому для всех объектов вызван метод класса State.");
    }

    private static void DemonstrateInit()
    {
        State state = ReadStateOfChosenType();
        state.Init();
        Console.WriteLine("Введённый объект:");
        state.Show();

        State copy = new State(state);
        Console.WriteLine($"Equals с копией, созданной конструктором копирования State: {state.Equals(copy)}");
        Console.WriteLine($"Equals с клоном (Clone): {state.Equals(state.Clone())}");
    }

    // ---------- Часть 2 ----------

    private static void RunQueries(State[] states)
    {
        ShowStates(states);
        Console.WriteLine();
        Console.WriteLine("1 - Имена монархов на заданном континенте");
        Console.WriteLine("2 - Республики со сроком полномочий президента не менее N лет");
        Console.WriteLine("3 - Количество государств заданного типа");
        switch (ReadInt("Номер запроса: ", 1, 3))
        {
            case 1:
                string continent = ReadContinent();
                PrintList("Монархи:", StateQueries.GetMonarchNames(states, continent));
                break;
            case 2:
                int minYears = ReadInt(
                    $"Минимальный срок ({Republic.MinTermYears}-{Republic.MaxTermYears} лет): ",
                    Republic.MinTermYears,
                    Republic.MaxTermYears);
                PrintList("Республики:", StateQueries.GetRepublicNames(states, minYears));
                break;
            case 3:
                int typeIndex = ReadTypeIndex();
                Console.WriteLine("Количество: " + StateQueries.CountStatesOfType(states, stateTypes[typeIndex]));
                break;
        }
    }

    // ---------- Часть 3 ----------

    private static void DemonstrateSortByName(State[] states)
    {
        State[] sorted = (State[])states.Clone();
        Array.Sort(sorted); // использует IComparable
        Console.WriteLine("Сортировка по названию (IComparable):");
        ShowStates(sorted);

        State key = new State();
        key.Name = Input.ReadText("Название для поиска: ");
        // Comparer.Default сравнивает объекты через IComparable
        PrintSearchResult(sorted, StateQueries.SearchBinary(sorted, key, Comparer.Default));
    }

    private static void DemonstrateSortByPopulation(State[] states)
    {
        var comparer = new SortByPopulation();
        State[] sorted = (State[])states.Clone();
        Array.Sort(sorted, comparer);
        Console.WriteLine("Сортировка по населению (IComparer):");
        ShowStates(sorted);

        State key = new State();
        key.Population = Input.ReadInt("Население для поиска: ");
        PrintSearchResult(sorted, StateQueries.SearchBinary(sorted, key, comparer));
    }

    private static void DemonstrateIInit()
    {
        // В одном массиве объекты иерархии и объект класса вне иерархии
        IInit[] items = { new State(), new Republic(), new Monarchy(), new Kingdom(), new Embassy() };
        foreach (IInit item in items)
        {
            item.RandomInit();
        }
        Console.WriteLine("После RandomInit():");
        ShowItems(items);

        int number = ReadInt("Номер элемента для ввода через Init() (0 - пропустить): ", 0, items.Length);
        if (number > 0)
        {
            items[number - 1].Init();
            Console.WriteLine("После Init():");
            ShowItems(items);
        }
    }

    private static void DemonstrateCloning()
    {
        Kingdom original = new Kingdom();
        original.RandomInit();
        original.Neighbors = new[] { "Альфия", "Бетания" };

        State shallowCopy = original.ShallowCopy();
        State deepCopy = (State)original.Clone();

        // Меняем название и первого соседа у оригинала
        original.Name = "Изменённое название";
        original.Neighbors[0] = "Изменённый сосед";

        Console.WriteLine("Оригинал:");
        original.Show();
        Console.WriteLine("Поверхностная копия (ShallowCopy):");
        shallowCopy.Show();
        Console.WriteLine("Глубокая копия (Clone):");
        deepCopy.Show();

        Console.WriteLine();
        Console.WriteLine("Массив соседей общий с оригиналом у ShallowCopy: " + ReferenceEquals(original.Neighbors, shallowCopy.Neighbors));
        Console.WriteLine("Массив соседей общий с оригиналом у Clone: " + ReferenceEquals(original.Neighbors, deepCopy.Neighbors));
        Console.WriteLine("Тип клона: " + deepCopy.GetType().Name);
    }

    // ---------- Ввод ----------

    private static int ReadInt(string prompt, int min, int max)
    {
        Console.Write(prompt);
        int value;
        while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max)
        {
            Console.Write($"Введите число от {min} до {max}: ");
        }
        return value;
    }

    private static int ReadTypeIndex()
    {
        for (int i = 0; i < stateTypeNames.Length; i++)
        {
            Console.WriteLine($"  {i + 1} - {stateTypeNames[i]}");
        }
        return ReadInt("Выберите тип: ", 1, stateTypes.Length) - 1;
    }

    private static string ReadContinent()
    {
        return Input.ReadText($"Континент ({RandomData.ContinentList}): ");
    }

    private static State ReadStateOfChosenType()
    {
        int typeIndex = ReadTypeIndex();
        return (State)Activator.CreateInstance(stateTypes[typeIndex]);
    }

    // ---------- Вывод ----------

    private static void PrintMenu()
    {
        Console.WriteLine("=== Иерархия государств ===");
        Console.WriteLine("1 - Показ массива: виртуальные и обычные методы");
        Console.WriteLine("2 - Ввод государства с клавиатуры (Init) и Equals");
        Console.WriteLine("3 - Запросы (is / as / typeof)");
        Console.WriteLine("4 - Сортировка по названию (IComparable) и бинарный поиск");
        Console.WriteLine("5 - Сортировка по населению (IComparer) и бинарный поиск");
        Console.WriteLine("6 - Массив IInit: Init() и RandomInit()");
        Console.WriteLine("7 - Клонирование и поверхностное копирование");
        Console.WriteLine("0 - Выход");
    }

    private static void ShowStates(State[] states)
    {
        for (int i = 0; i < states.Length; i++)
        {
            Console.WriteLine($"[{i}]");
            states[i].Show();
        }
    }

    private static void ShowItems(IInit[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i].GetType().Name}");
            // Определяем реальный тип элемента во время выполнения
            if (items[i] is State state)
            {
                state.Show();
            }
            else if (items[i] is Embassy embassy)
            {
                embassy.Show();
            }
        }
    }

    private static void PrintList(string title, List<string> items)
    {
        Console.WriteLine(title);
        if (items.Count == 0)
        {
            Console.WriteLine("  (ничего не найдено)");
        }
        foreach (string item in items)
        {
            Console.WriteLine("  " + item);
        }
    }

    private static void PrintSearchResult(State[] states, int index)
    {
        if (index < 0)
        {
            Console.WriteLine("Элемент не найден.");
            return;
        }
        Console.WriteLine("Найден, индекс: " + index);
        states[index].Show();
    }
}