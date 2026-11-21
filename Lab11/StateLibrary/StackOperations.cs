using System;
using System.Collections;
using System.Collections.Generic;

namespace StateLibrary;

// Операции над обобщённым стеком Stack<State>.
// Стек не поддерживает доступ по индексу, сортировку и двоичный поиск, поэтому они реализованы здесь
public static class StackOperations
{
    public static Stack<State> CreateRandomStack(int count)
    {
        // Конструктор Stack кладёт элементы по очереди: последний окажется на вершине
        return new Stack<State>(StateFactory.CreateRandomStates(count));
    }

    public static void SortByName(Stack<State> states)
    {
        Sort(states, null); // null: сравнение через IComparable
    }

    public static void SortByPopulation(Stack<State> states)
    {
        Sort(states, new SortByPopulation());
    }

    // Линейный поиск от вершины стека: 0 - вершина, -1 - государство не найдено
    public static int SearchByName(Stack<State> states, string name)
    {
        int depth = 0;
        foreach (State state in states)
        {
            if (state.Name == name)
            {
                return depth;
            }
            depth++;
        }
        return -1;
    }

    // Поверхностное клонирование: новый стек с теми же объектами
    public static Stack<State> CloneShallow(Stack<State> states)
    {
        return CopyStack(states, false);
    }

    // Глубокое клонирование: объекты в новом стеке тоже копии
    public static Stack<State> CloneDeep(Stack<State> states)
    {
        return CopyStack(states, true);
    }

    // Сортирует на месте: элементы переносятся в массив, сортируются и кладутся обратно
    // так, чтобы на вершине оказался наименьший
    private static void Sort(Stack<State> states, IComparer comparer)
    {
        State[] items = states.ToArray(); // порядок от вершины к основанию
        Array.Sort(items, comparer);
        states.Clear();
        for (int i = items.Length - 1; i >= 0; i--)
        {
            states.Push(items[i]);
        }
    }

    private static Stack<State> CopyStack(Stack<State> states, bool isDeep)
    {
        State[] items = states.ToArray(); // от вершины к основанию
        Array.Reverse(items);             // от основания к вершине: так элементы кладутся в новый стек
        if (isDeep)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = (State)items[i].Clone();
            }
        }
        return new Stack<State>(items);
    }
}