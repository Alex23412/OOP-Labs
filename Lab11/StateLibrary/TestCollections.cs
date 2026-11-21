using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StateLibrary;

// Четыре коллекции с одними и теми же республиками для сравнения скорости поиска.
// Республика (производный класс) - значение, её базовая часть State - ключ словаря.
public class TestCollections
{
    public const int DefaultCount = 1000;
    private const int MeasureRuns = 11;

    private readonly LinkedList<Republic> republicList = new LinkedList<Republic>();
    private readonly LinkedList<string> stringList = new LinkedList<string>();
    private readonly SortedDictionary<State, Republic> republicDictionary = new SortedDictionary<State, Republic>();
    private readonly SortedDictionary<string, Republic> stringDictionary = new SortedDictionary<string, Republic>();
    private int nextNumber;

    public TestCollections(int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddRandomElement();
        }
    }

    // Во всех четырёх коллекциях всегда одинаковое число элементов
    public int Count
    {
        get => republicList.Count;
    }

    public int StringListCount
    {
        get => stringList.Count;
    }

    public int DictionaryCount
    {
        get => republicDictionary.Count;
    }

    public int StringDictionaryCount
    {
        get => stringDictionary.Count;
    }

    // Создаёт случайную республику и добавляет её во все коллекции
    public Republic AddRandomElement()
    {
        var republic = new Republic();
        republic.RandomInit();
        // Номер в названии делает названия, а значит и ключи словарей, уникальными
        nextNumber++;
        republic.Name = $"{nextNumber:D6} {republic.Name}";
        AddElement(republic);
        return republic;
    }

    // Добавляет республику во все четыре коллекции; false, если такой ключ уже есть
    public bool AddElement(Republic republic)
    {
        ArgumentNullException.ThrowIfNull(republic);
        string text = republic.ToString();
        if (republicDictionary.ContainsKey(republic.BaseState) || stringDictionary.ContainsKey(text))
        {
            return false;
        }
        republicList.AddLast(republic);
        stringList.AddLast(text);
        republicDictionary.Add(republic.BaseState, republic);
        stringDictionary.Add(text, republic);
        return true;
    }

    // Удаляет республику (по содержимому) из всех четырёх коллекций; false, если её не было
    public bool RemoveElement(Republic republic)
    {
        ArgumentNullException.ThrowIfNull(republic);
        string text = republic.ToString();
        bool isRemoved = republicList.Remove(republic);
        stringList.Remove(text);
        republicDictionary.Remove(republic.BaseState);
        stringDictionary.Remove(text);
        return isRemoved;
    }

    public bool RemoveElementAt(int index)
    {
        return RemoveElement(GetElementCopy(index));
    }

    public Republic GetFirstElement()
    {
        return GetElementCopy(0);
    }

    public Republic GetCenterElement()
    {
        return GetElementCopy(Count / 2);
    }

    public Republic GetLastElement()
    {
        return GetElementCopy(Count - 1);
    }

    // Республика, которой заведомо нет в коллекциях
    public Republic CreateAbsentElement()
    {
        return new Republic("Отсутствующее государство", 1, "Европа", new string[0], "Никто", 4);
    }

    // Измеряет время поиска элемента в коллекциях. Ищется содержимое (копия), а не ссылка
    public SearchTimes MeasureSearchTimes(Republic element)
    {
        ArgumentNullException.ThrowIfNull(element);
        string text = element.ToString();
        State key = element.BaseState;
        var times = new SearchTimes();
        times.ListContains = MeasureTicks(() => republicList.Contains(element));
        times.StringListContains = MeasureTicks(() => stringList.Contains(text));
        times.DictionaryContainsKey = MeasureTicks(() => republicDictionary.ContainsKey(key));
        times.StringDictionaryContainsKey = MeasureTicks(() => stringDictionary.ContainsKey(text));
        times.DictionaryContainsValue = MeasureTicks(() => republicDictionary.ContainsValue(element));
        return times;
    }

    // Копия элемента списка с номером index (0 - первый), а не ссылка на сам элемент
    private Republic GetElementCopy(int index)
    {
        if (index < 0 || index >= republicList.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        LinkedListNode<Republic> node = republicList.First;
        for (int i = 0; i < index; i++)
        {
            node = node.Next;
        }
        return new Republic(node.Value);
    }

    // Время одного поиска в тиках. Первый запуск - прогрев (JIT-компиляция не должна попасть
    // в измерение); дальше берётся медиана нескольких замеров, чтобы случайные задержки не искажали результат
    private static long MeasureTicks(Action search)
    {
        search();
        var ticks = new long[MeasureRuns];
        for (int i = 0; i < MeasureRuns; i++)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            search();
            stopwatch.Stop();
            ticks[i] = stopwatch.ElapsedTicks;
        }
        Array.Sort(ticks);
        return ticks[MeasureRuns / 2];
    }
}