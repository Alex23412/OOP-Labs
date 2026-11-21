using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using StateLibrary;

namespace StateApp;

internal class Program
{
    private const int StartCount = 8;

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        

        // Коллекции заданий 1 и 2 сразу содержат объекты всех типов иерархии
        ArrayList arrayList = ArrayListOperations.CreateRandomList(StartCount);
        Stack<State> stack = StackOperations.CreateRandomStack(StartCount);
        var testCollections = new TestCollections(TestCollections.DefaultCount);

        bool isRunning = true;
        while (isRunning)
        {
            PrintMainMenu();
            int choice = ReadInt("Ваш выбор: ", 0, 3);
            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    RunArrayListMenu(arrayList);
                    break;
                case 2:
                    RunStackMenu(stack);
                    break;
                case 3:
                    RunTestCollectionsMenu(testCollections);
                    break;
                default:
                    isRunning = false;
                    break;
            }
        }
    }

    // ---------- Задание 1: ArrayList ----------

    private static void RunArrayListMenu(ArrayList states)
    {
        bool isRunning = true;
        while (isRunning)
        {
            PrintArrayListMenu();
            int choice = ReadInt("Ваш выбор: ", 0, 9);
            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    ShowCollection(states, "ArrayList:");
                    break;
                case 2:
                    states.Add(StateFactory.CreateRandomState());
                    Console.WriteLine("Случайный объект добавлен.");
                    break;
                case 3:
                    states.Add(ReadStateFromKeyboard());
                    Console.WriteLine("Объект добавлен.");
                    break;
                case 4:
                    RemoveArrayListItemByIndex(states);
                    break;
                case 5:
                    RemoveArrayListItemByName(states);
                    break;
                case 6:
                    RunQueries(states);
                    break;
                case 7:
                    DemonstrateArrayListCloning(states);
                    break;
                case 8:
                    SortAndSearchArrayListByName(states);
                    break;
                case 9:
                    ArrayListOperations.SortByPopulation(states);
                    ShowCollection(states, "ArrayList после сортировки по населению (IComparer):");
                    break;
                default:
                    isRunning = false;
                    break;
            }
            Console.WriteLine();
        }
    }

    private static void RemoveArrayListItemByIndex(ArrayList states)
    {
        if (states.Count == 0)
        {
            Console.WriteLine("Коллекция пуста.");
            return;
        }
        int index = ReadInt($"Номер удаляемого объекта (0..{states.Count - 1}): ", 0, states.Count - 1);
        states.RemoveAt(index);
        Console.WriteLine("Объект удалён.");
    }

    private static void RemoveArrayListItemByName(ArrayList states)
    {
        string name = Input.ReadText("Название удаляемого государства: ").Trim();
        bool isRemoved = ArrayListOperations.RemoveByName(states, name);
        Console.WriteLine(isRemoved ? "Объект удалён." : "Объект с таким названием не найден.");
    }

    private static void DemonstrateArrayListCloning(ArrayList states)
    {
        if (states.Count == 0)
        {
            Console.WriteLine("Коллекция пуста.");
            return;
        }
        ArrayList shallowCopy = ArrayListOperations.CloneShallow(states);
        ArrayList deepCopy = ArrayListOperations.CloneDeep(states);
        ShowCloneEffect((State)states[0], (State)shallowCopy[0], (State)deepCopy[0]);
    }

    private static void SortAndSearchArrayListByName(ArrayList states)
    {
        ArrayListOperations.SortByName(states);
        ShowCollection(states, "ArrayList после сортировки по названию (IComparable):");
        string name = Input.ReadText("Название для двоичного поиска: ").Trim();
        int index = ArrayListOperations.SearchByName(states, name);
        PrintSearchResult(index, index >= 0 ? (State)states[index] : null);
    }

    // ---------- Задание 2: Stack<State> ----------

    private static void RunStackMenu(Stack<State> states)
    {
        bool isRunning = true;
        while (isRunning)
        {
            PrintStackMenu();
            int choice = ReadInt("Ваш выбор: ", 0, 9);
            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    ShowCollection(states, "Stack<State> (сверху вниз):");
                    break;
                case 2:
                    states.Push(StateFactory.CreateRandomState());
                    Console.WriteLine("Случайный объект помещён в стек.");
                    break;
                case 3:
                    states.Push(ReadStateFromKeyboard());
                    Console.WriteLine("Объект помещён в стек.");
                    break;
                case 4:
                    PopFromStack(states);
                    break;
                case 5:
                    PeekStack(states);
                    break;
                case 6:
                    RunQueries(states);
                    break;
                case 7:
                    DemonstrateStackCloning(states);
                    break;
                case 8:
                    SortAndSearchStackByName(states);
                    break;
                case 9:
                    StackOperations.SortByPopulation(states);
                    ShowCollection(states, "Stack<State> после сортировки по населению (IComparer):");
                    break;
                default:
                    isRunning = false;
                    break;
            }
            Console.WriteLine();
        }
    }

    private static void PopFromStack(Stack<State> states)
    {
        if (states.Count == 0)
        {
            Console.WriteLine("Стек пуст.");
            return;
        }
        State removed = states.Pop();
        Console.WriteLine("Удалён верхний объект:");
        removed.Show();
    }

    private static void PeekStack(Stack<State> states)
    {
        if (states.Count == 0)
        {
            Console.WriteLine("Стек пуст.");
            return;
        }
        Console.WriteLine("Верхний объект (не удаляется):");
        states.Peek().Show();
    }

    private static void DemonstrateStackCloning(Stack<State> states)
    {
        if (states.Count == 0)
        {
            Console.WriteLine("Стек пуст.");
            return;
        }
        Stack<State> shallowCopy = StackOperations.CloneShallow(states);
        Stack<State> deepCopy = StackOperations.CloneDeep(states);
        ShowCloneEffect(states.Peek(), shallowCopy.Peek(), deepCopy.Peek());
    }

    private static void SortAndSearchStackByName(Stack<State> states)
    {
        StackOperations.SortByName(states);
        ShowCollection(states, "Stack<State> после сортировки по названию (IComparable):");
        string name = Input.ReadText("Название для поиска: ").Trim();
        int depth = StackOperations.SearchByName(states, name);
        PrintSearchResult(depth, depth >= 0 ? states.ToArray()[depth] : null);
    }

    // ---------- Запросы (общие для заданий 1 и 2) ----------

    private static void RunQueries(IEnumerable states)
    {
        Console.WriteLine("Запросы:");
        Console.WriteLine("1 - Имена монархов на заданном континенте");
        Console.WriteLine("2 - Количество государств заданного типа");
        Console.WriteLine("3 - Государства с населением не меньше заданного");
        switch (ReadInt("Номер запроса: ", 1, 3))
        {
            case 1:
                string continent = ReadContinent();
                PrintList("Монархи:", StateQueries.GetMonarchNames(states, continent));
                break;
            case 2:
                int typeIndex = ReadTypeIndex();
                Type[] types = StateFactory.GetStateTypes();
                Console.WriteLine("Количество: " + StateQueries.CountStatesOfType(states, types[typeIndex]));
                break;
            case 3:
                int minPopulation = ReadInt("Минимальное население: ", 1, int.MaxValue);
                ShowCollection(StateQueries.GetStatesWithPopulationAtLeast(states, minPopulation), "Найденные государства:");
                break;
        }
    }

    // ---------- Задание 3: TestCollections ----------

    private static void RunTestCollectionsMenu(TestCollections testCollections)
    {
        bool isRunning = true;
        while (isRunning)
        {
            PrintTestCollectionsMenu();
            int choice = ReadInt("Ваш выбор: ", 0, 4);
            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    ShowCounts(testCollections);
                    break;
                case 2:
                    testCollections.AddRandomElement();
                    Console.WriteLine("Случайная республика добавлена во все коллекции.");
                    break;
                case 3:
                    RemoveTestCollectionsItem(testCollections);
                    break;
                case 4:
                    MeasureSearch(testCollections);
                    break;
                default:
                    isRunning = false;
                    break;
            }
            Console.WriteLine();
        }
    }

    private static void RemoveTestCollectionsItem(TestCollections testCollections)
    {
        if (testCollections.Count == 0)
        {
            Console.WriteLine("Коллекции пусты.");
            return;
        }
        int index = ReadInt($"Номер элемента (0..{testCollections.Count - 1}): ", 0, testCollections.Count - 1);
        bool isRemoved = testCollections.RemoveElementAt(index);
        Console.WriteLine(isRemoved ? "Элемент удалён из всех коллекций." : "Элемент не найден.");
    }

    private static void MeasureSearch(TestCollections testCollections)
    {
        if (testCollections.Count == 0)
        {
            Console.WriteLine("Коллекции пусты.");
            return;
        }
        Republic[] elements =
        {
            testCollections.GetFirstElement(),
            testCollections.GetCenterElement(),
            testCollections.GetLastElement(),
            testCollections.CreateAbsentElement()
        };
        string[] names = { "Первый", "Центральный", "Последний", "Отсутствует" };

        PrintSearchHeader();
        for (int i = 0; i < elements.Length; i++)
        {
            PrintSearchRow(names[i], testCollections.MeasureSearchTimes(elements[i]));
        }
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
        string[] typeNames = StateFactory.GetStateTypeNames();
        for (int i = 0; i < typeNames.Length; i++)
        {
            Console.WriteLine($"  {i + 1} - {typeNames[i]}");
        }
        return ReadInt("Выберите тип: ", 1, typeNames.Length) - 1;
    }

    private static string ReadContinent()
    {
        return Input.ReadText($"Континент ({RandomData.ContinentList}): ").Trim();
    }

    private static State ReadStateFromKeyboard()
    {
        int typeIndex = ReadTypeIndex();
        State state = StateFactory.CreateState(StateFactory.GetStateTypes()[typeIndex]);
        state.Init();
        return state;
    }

    // ---------- Вывод ----------

    private static void PrintMainMenu()
    {
        Console.WriteLine("=== Коллекции (вариант 12) ===");
        Console.WriteLine("1 - Задание 1: коллекция ArrayList");
        Console.WriteLine("2 - Задание 2: обобщённая коллекция Stack<State>");
        Console.WriteLine("3 - Задание 3: сравнение скорости поиска (LinkedList, SortedDictionary)");
        Console.WriteLine("0 - Выход");
    }

    private static void PrintArrayListMenu()
    {
        Console.WriteLine("=== Задание 1: ArrayList ===");
        Console.WriteLine("1 - Показать коллекцию (foreach)");
        Console.WriteLine("2 - Добавить случайный объект");
        Console.WriteLine("3 - Добавить объект, введённый с клавиатуры");
        Console.WriteLine("4 - Удалить объект по номеру");
        Console.WriteLine("5 - Удалить объект по названию");
        Console.WriteLine("6 - Запросы");
        Console.WriteLine("7 - Клонирование коллекции");
        Console.WriteLine("8 - Сортировка по названию и двоичный поиск");
        Console.WriteLine("9 - Сортировка по населению");
        Console.WriteLine("0 - Назад");
    }

    private static void PrintStackMenu()
    {
        Console.WriteLine("=== Задание 2: Stack<State> ===");
        Console.WriteLine("1 - Показать коллекцию (foreach)");
        Console.WriteLine("2 - Добавить случайный объект (Push)");
        Console.WriteLine("3 - Добавить объект, введённый с клавиатуры (Push)");
        Console.WriteLine("4 - Удалить верхний объект (Pop)");
        Console.WriteLine("5 - Показать верхний объект (Peek)");
        Console.WriteLine("6 - Запросы");
        Console.WriteLine("7 - Клонирование коллекции");
        Console.WriteLine("8 - Сортировка по названию и поиск");
        Console.WriteLine("9 - Сортировка по населению");
        Console.WriteLine("0 - Назад");
    }

    private static void PrintTestCollectionsMenu()
    {
        Console.WriteLine("=== Задание 3: TestCollections ===");
        Console.WriteLine("1 - Показать размеры коллекций");
        Console.WriteLine("2 - Добавить случайный элемент во все коллекции");
        Console.WriteLine("3 - Удалить элемент по номеру из всех коллекций");
        Console.WriteLine("4 - Измерить время поиска");
        Console.WriteLine("0 - Назад");
    }

    // Перебор элементов коллекции оператором foreach
    private static void ShowCollection(IEnumerable states, string title)
    {
        Console.WriteLine(title);
        int number = 0;
        foreach (State state in states)
        {
            Console.WriteLine($"[{number}]");
            state.Show();
            number++;
        }
        if (number == 0)
        {
            Console.WriteLine("(коллекция пуста)");
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

    private static void PrintSearchResult(int position, State found)
    {
        if (position < 0)
        {
            Console.WriteLine("Элемент не найден.");
            return;
        }
        Console.WriteLine("Элемент найден, позиция: " + position);
        found.Show();
    }

    // Название первого элемента меняется только у оригинала: так видно, делит ли копия объекты с оригиналом
    private static void ShowCloneEffect(State original, State shallowItem, State deepItem)
    {
        string oldName = original.Name;
        original.Name = "Изменённое название";
        Console.WriteLine("Первый элемент после изменения названия в оригинале:");
        Console.WriteLine("  оригинал:            " + original.Name);
        Console.WriteLine("  поверхностная копия: " + shallowItem.Name);
        Console.WriteLine("  глубокая копия:      " + deepItem.Name);
        original.Name = oldName;
    }

    private static void ShowCounts(TestCollections testCollections)
    {
        Console.WriteLine("Размеры коллекций:");
        Console.WriteLine("  LinkedList<Republic>:                  " + testCollections.Count);
        Console.WriteLine("  LinkedList<string>:                    " + testCollections.StringListCount);
        Console.WriteLine("  SortedDictionary<State, Republic>:     " + testCollections.DictionaryCount);
        Console.WriteLine("  SortedDictionary<string, Republic>:    " + testCollections.StringDictionaryCount);
    }

    private static void PrintSearchHeader()
    {
        Console.WriteLine("Время поиска в тиках Stopwatch (медиана нескольких замеров):");
        Console.WriteLine("  A - LinkedList<Republic>.Contains");
        Console.WriteLine("  B - LinkedList<string>.Contains");
        Console.WriteLine("  C - SortedDictionary<State, Republic>.ContainsKey");
        Console.WriteLine("  D - SortedDictionary<string, Republic>.ContainsKey");
        Console.WriteLine("  E - SortedDictionary<State, Republic>.ContainsValue");
        Console.WriteLine(string.Format("{0,-14}{1,10}{2,10}{3,10}{4,10}{5,10}", "Элемент", "A", "B", "C", "D", "E"));
    }

    private static void PrintSearchRow(string name, SearchTimes times)
    {
        Console.WriteLine(string.Format("{0,-14}{1,10}{2,10}{3,10}{4,10}{5,10}", name, times.ListContains,
            times.StringListContains, times.DictionaryContainsKey, times.StringDictionaryContainsKey,
            times.DictionaryContainsValue));
    }
}