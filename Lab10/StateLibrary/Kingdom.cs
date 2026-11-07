using System;

namespace StateLibrary;

// Королевство (монархия с наследником)
public class Kingdom : Monarchy
{
    private string heirName;

    public Kingdom() : base()
    {
        HeirName = TextHelper.DefaultText;
    }

    public Kingdom(string name, int population, string continent, string[] neighbors,
        string monarchName, string dynasty, string heirName)
        : base(name, population, continent, neighbors, monarchName, dynasty)
    {
        HeirName = heirName;
    }

    public Kingdom(Kingdom other) : base(other)
    {
        HeirName = other.HeirName;
    }

    public string HeirName
    {
        get => heirName;
        set => heirName = TextHelper.GetTextOrDefault(value);
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"  Королевство: наследник {HeirName}");
    }

    public new void ShowShortInfo()
    {
        Console.WriteLine($"Королевство {Name}, монарх {MonarchName}, наследник {HeirName}");
    }

    public override void Init()
    {
        base.Init();
        HeirName = Input.ReadText("Имя наследника: ");
    }

    public override void RandomInit()
    {
        base.RandomInit();
        HeirName = RandomData.GetPersonName();
    }

    public override bool Equals(object obj)
    {
        if (!base.Equals(obj))
        {
            return false;
        }

        Kingdom other = (Kingdom)obj;
        return HeirName == other.HeirName;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override object Clone()
    {
        return new Kingdom(this);
    }
}
