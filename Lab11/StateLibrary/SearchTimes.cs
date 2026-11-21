namespace StateLibrary;

// Время поиска одного элемента в пяти вариантах (в тиках Stopwatch)
public class SearchTimes
{
    public long ListContains { get; set; }

    public long StringListContains { get; set; }

    public long DictionaryContainsKey { get; set; }

    public long StringDictionaryContainsKey { get; set; }

    public long DictionaryContainsValue { get; set; }
}