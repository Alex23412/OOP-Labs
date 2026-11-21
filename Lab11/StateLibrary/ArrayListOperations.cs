using System.Collections;

namespace StateLibrary;

// Операции над коллекцией ArrayList, в которой лежат объекты иерархии State.
// ArrayList хранит ссылки типа object, поэтому при чтении нужно приводить элемент к State
public static class ArrayListOperations
{
    public static ArrayList CreateRandomList(int count)
    {
        return new ArrayList(StateFactory.CreateRandomStates(count));
    }

    // Удаляет первое государство с заданным названием; false, если такого нет
    public static bool RemoveByName(ArrayList states, string name)
    {
        for (int i = 0; i < states.Count; i++)
        {
            if (((State)states[i]).Name == name)
            {
                states.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public static void SortByName(ArrayList states)
    {
        states.Sort(); // использует IComparable.CompareTo из класса State
    }

    public static void SortByPopulation(ArrayList states)
    {
        states.Sort(new SortByPopulation());
    }

    // Двоичный поиск по названию; список должен быть отсортирован по названию.
    // Возвращает индекс элемента или -1
    public static int SearchByName(ArrayList sortedStates, string name)
    {
        var key = new State();
        key.Name = name;
        int index = sortedStates.BinarySearch(key);
        return index >= 0 ? index : -1;
    }

    // Поверхностное клонирование: новая коллекция, но элементы те же самые объекты
    public static ArrayList CloneShallow(ArrayList states)
    {
        return (ArrayList)states.Clone();
    }

    // Глубокое клонирование: каждый элемент тоже копируется
    public static ArrayList CloneDeep(ArrayList states)
    {
        var copy = new ArrayList(states.Count);
        foreach (State state in states)
        {
            copy.Add(state.Clone());
        }
        return copy;
    }
}