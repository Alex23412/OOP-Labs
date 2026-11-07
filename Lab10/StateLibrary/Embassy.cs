using System;

namespace StateLibrary;

// Класс вне иерархии, реализующий IInit
public class Embassy : IInit
{
    private string city;
    private string ambassadorName;

    public Embassy()
    {
        City = TextHelper.DefaultText;
        AmbassadorName = TextHelper.DefaultText;
    }

    public Embassy(string city, string ambassadorName)
    {
        City = city;
        AmbassadorName = ambassadorName;
    }

    public Embassy(Embassy other)
    {
        City = other.City;
        AmbassadorName = other.AmbassadorName;
    }

    public string City
    {
        get => city;
        set => city = TextHelper.GetTextOrDefault(value);
    }

    public string AmbassadorName
    {
        get => ambassadorName;
        set => ambassadorName = TextHelper.GetTextOrDefault(value);
    }

    public void Show()
    {
        Console.WriteLine($"Посольство в городе {City}, посол {AmbassadorName}");
    }

    public void Init()
    {
        City = Input.ReadText("Город посольства: ");
        AmbassadorName = Input.ReadText("Имя посла: ");
    }

    public void RandomInit()
    {
        City = RandomData.GetStateName();
        AmbassadorName = RandomData.GetPersonName();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Embassy other = (Embassy)obj;
        return City == other.City && AmbassadorName == other.AmbassadorName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(City, AmbassadorName);
    }
}
