using System.Collections;

namespace StateLibrary;

// Критерий сортировки для Array.Sort (IComparer)
public class SortByPopulation : IComparer
{
    public int Compare(object first, object second)
    {
        State firstState = (State)first;
        State secondState = (State)second;
        return firstState.Population.CompareTo(secondState.Population);
    }
}
