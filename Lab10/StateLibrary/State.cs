using System;
using System.Linq;

namespace StateLibrary;

// Базовый класс: государство
public class State : IInit, IComparable, ICloneable
{
    public const int MinPopulation = 1;

    private string name;
    private int population;
    private string continent;
    // Ссылочное поле: нужно, чтобы показать разницу между глубоким и поверхностным копированием
    private string[] neighbors;

    public State()
    {
        Name = TextHelper.DefaultText;
        Population = MinPopulation;
        Continent = TextHelper.DefaultText;
        Neighbors = new string[0];
    }

    public State(string name, int population, string continent, string[] neighbors)
    {
        Name = name;
        Population = population;
        Continent = continent;
        Neighbors = neighbors;
    }

    // Конструктор копирования
    public State(State other)
    {
        Name = other.Name;
        Population = other.Population;
        Continent = other.Continent;
        Neighbors = other.Neighbors;
    }

    // В свойствах проверяются ограничения: неверное значение заменяется допустимым
    public string Name
    {
        get => name;
        set => name = TextHelper.GetTextOrDefault(value);
    }

    public int Population
    {
        get => population;
        set => population = value >= MinPopulation ? value : MinPopulation;
    }

    public string Continent
    {
        get => continent;
        set => continent = TextHelper.GetTextOrDefault(value);
    }

    // Сеттер сохраняет копию массива, поэтому конструкторы не делят массив с источником
    public string[] Neighbors
    {
        get => neighbors;
        set => neighbors = value == null ? new string[0] : (string[])value.Clone();
    }

    // Виртуальный метод: версия выбирается по фактическому типу объекта
    public virtual void Show()
    {
        Console.WriteLine($"{Name}: {Continent}, население {Population}, соседи: {string.Join(", ", Neighbors)}");
    }

    // Обычный метод: версия выбирается по типу ссылки (в наследниках он скрыт через new)
    public void ShowShortInfo()
    {
        Console.WriteLine($"Государство {Name}");
    }

    public virtual void Init()
    {
        Name = Input.ReadText("Название: ");
        Population = Input.ReadInt("Население: ");
        Continent = Input.ReadText("Континент: ");
        string line = Input.ReadText("Соседи (через запятую): ");
        Neighbors = line.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    public virtual void RandomInit()
    {
        Name = RandomData.GetStateName();
        Population = RandomData.GetPopulation();
        Continent = RandomData.GetContinent();
        Neighbors = RandomData.GetNeighbors(Name);
    }

    // Объекты равны, если совпадают типы и все поля
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        State other = (State)obj;
        return Name == other.Name
            && Population == other.Population
            && Continent == other.Continent
            && Neighbors.SequenceEqual(other.Neighbors);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Population, Continent);
    }

    // Сравнение по названию (для Array.Sort)
    public int CompareTo(object obj)
    {
        if (obj == null)
        {
            return 1;
        }

        State other = (State)obj;
        return string.Compare(Name, other.Name, StringComparison.Ordinal);
    }

    // Глубокое клонирование: копируется и массив соседей
    public virtual object Clone()
    {
        return new State(this);
    }

    // Поверхностное копирование: массив соседей остаётся общим
    public State ShallowCopy()
    {
        return (State)MemberwiseClone();
    }
}
