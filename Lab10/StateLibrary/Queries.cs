using System;
using System.Collections;
using System.Collections.Generic;

namespace StateLibrary;

// Запросы к массиву государств и бинарный поиск. Ввода и вывода здесь нет.
public static class StateQueries
{
    // Имена монархов на континенте. Оператор as отбирает и Monarchy, и Kingdom (он наследник Monarchy)
    public static List<string> GetMonarchNames(State[] states, string continent)
    {
        var names = new List<string>();
        foreach (State state in states)
        {
            Monarchy monarchy = state as Monarchy;
            if (monarchy != null && string.Equals(monarchy.Continent, continent, StringComparison.OrdinalIgnoreCase))
            {
                names.Add(monarchy.MonarchName);
            }
        }
        return names;
    }

    // Названия республик со сроком полномочий президента не менее заданного
    public static List<string> GetRepublicNames(State[] states, int minTermYears)
    {
        var names = new List<string>();
        foreach (State state in states)
        {
            if (state is Republic republic && republic.TermYears >= minTermYears)
            {
                names.Add(republic.Name);
            }
        }
        return names;
    }

    // Количество объектов ТОЧНО заданного типа (typeof не засчитывает наследников, в отличие от is)
    public static int CountStatesOfType(State[] states, Type stateType)
    {
        int count = 0;
        foreach (State state in states)
        {
            if (state.GetType() == stateType)
            {
                count++;
            }
        }
        return count;
    }

    // Бинарный поиск в массиве, отсортированном по тому же критерию, что и comparer.
    // Возвращает индекс найденного элемента или -1.
    public static int SearchBinary(State[] sortedStates, State key, IComparer comparer)
    {
        int low = 0;
        int high = sortedStates.Length - 1;
        while (low <= high)
        {
            int middle = (low + high) / 2;
            int comparison = comparer.Compare(sortedStates[middle], key);
            if (comparison == 0)
            {
                return middle;
            }
            if (comparison < 0)
            {
                low = middle + 1; // средний элемент меньше ключа, ищем справа
            }
            else
            {
                high = middle - 1; // средний элемент больше ключа, ищем слева
            }
        }
        return -1;
    }
}
