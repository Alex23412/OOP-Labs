using System;

namespace StateLibrary;

// Монархия
public class Monarchy : State
{
    private string monarchName;
    private string dynasty;

    public Monarchy() : base()
    {
        MonarchName = TextHelper.DefaultText;
        Dynasty = TextHelper.DefaultText;
    }

    public Monarchy(string name, int population, string continent, string[] neighbors,
        string monarchName, string dynasty) : base(name, population, continent, neighbors)
    {
        MonarchName = monarchName;
        Dynasty = dynasty;
    }

    public Monarchy(Monarchy other) : base(other)
    {
        MonarchName = other.MonarchName;
        Dynasty = other.Dynasty;
    }

    public string MonarchName
    {
        get => monarchName;
        set => monarchName = TextHelper.GetTextOrDefault(value);
    }

    public string Dynasty
    {
        get => dynasty;
        set => dynasty = TextHelper.GetTextOrDefault(value);
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"  Монархия: монарх {MonarchName}, династия {Dynasty}");
    }

    public new void ShowShortInfo()
    {
        Console.WriteLine($"Монархия {Name}, монарх {MonarchName}");
    }

    public override void Init()
    {
        base.Init();
        MonarchName = Input.ReadText("Имя монарха: ");
        Dynasty = Input.ReadText("Династия: ");
    }

    public override void RandomInit()
    {
        base.RandomInit();
        MonarchName = RandomData.GetPersonName();
        Dynasty = RandomData.GetDynasty();
    }

    public override bool Equals(object obj)
    {
        if (!base.Equals(obj))
        {
            return false;
        }

        Monarchy other = (Monarchy)obj;
        return MonarchName == other.MonarchName && Dynasty == other.Dynasty;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override object Clone()
    {
        return new Monarchy(this);
    }
}
