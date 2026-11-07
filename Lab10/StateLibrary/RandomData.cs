using System;
using System.Linq;

namespace StateLibrary;

// Датчик случайных данных для методов RandomInit()
public static class RandomData
{
    private const int MaxNeighborsCount = 3;

    private static readonly Random generator = new Random();
    private static readonly string[] stateNames = { "Аурелия", "Бореалия", "Кардония", "Данвия", "Эльдория", "Фарланд", "Гирния", "Хальмар" };
    private static readonly string[] continents = { "Европа", "Азия", "Африка", "Америка", "Австралия" };
    private static readonly string[] personNames = { "Александр Вернер", "Мария Лангер", "Эдуард Рихтер", "Ингрид Хольм", "Томас Виртанен" };
    private static readonly string[] dynasties = { "Эрлинги", "Ренфорды", "Альдеберги", "Тарнхеймы" };

    // Список континентов для подсказки при вводе
    public static string ContinentList => string.Join(", ", continents);

    public static string GetStateName() => PickFrom(stateNames);

    public static string GetContinent() => PickFrom(continents);

    public static string GetPersonName() => PickFrom(personNames);

    public static string GetDynasty() => PickFrom(dynasties);

    public static int GetPopulation() => generator.Next(100_000, 150_000_000);

    // Случайное целое из отрезка [min, max]
    public static int GetInt(int min, int max) => generator.Next(min, max + 1);

    // От 0 до 3 соседей; среди них нет самого государства
    public static string[] GetNeighbors(string excludedName)
    {
        return stateNames
            .Where(neighborName => neighborName != excludedName)
            .OrderBy(neighborName => generator.Next())
            .Take(GetInt(0, MaxNeighborsCount))
            .ToArray();
    }

    private static string PickFrom(string[] items)
    {
        return items[generator.Next(items.Length)];
    }
}
