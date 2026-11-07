using System;

namespace StateLibrary;

// Республика
public class Republic : State
{
    public const int MinTermYears = 1;
    public const int MaxTermYears = 10;
    private const int DefaultTermYears = 4;

    private string presidentName;
    private int termYears;

    public Republic() : base()
    {
        PresidentName = TextHelper.DefaultText;
        TermYears = DefaultTermYears;
    }

    public Republic(string name, int population, string continent, string[] neighbors,
        string presidentName, int termYears) : base(name, population, continent, neighbors)
    {
        PresidentName = presidentName;
        TermYears = termYears;
    }

    public Republic(Republic other) : base(other)
    {
        PresidentName = other.PresidentName;
        TermYears = other.TermYears;
    }

    public string PresidentName
    {
        get => presidentName;
        set => presidentName = TextHelper.GetTextOrDefault(value);
    }

    // Срок полномочий президента в годах
    public int TermYears
    {
        get => termYears;
        set => termYears = value >= MinTermYears && value <= MaxTermYears ? value : DefaultTermYears;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"  Республика: президент {PresidentName}, срок полномочий (лет): {TermYears}");
    }

    public new void ShowShortInfo()
    {
        Console.WriteLine($"Республика {Name}, президент {PresidentName}");
    }

    public override void Init()
    {
        base.Init();
        PresidentName = Input.ReadText("Имя президента: ");
        TermYears = Input.ReadInt($"Срок полномочий ({MinTermYears}-{MaxTermYears} лет): ");
    }

    public override void RandomInit()
    {
        base.RandomInit();
        PresidentName = RandomData.GetPersonName();
        TermYears = RandomData.GetInt(MinTermYears, MaxTermYears);
    }

    public override bool Equals(object obj)
    {
        if (!base.Equals(obj))
        {
            return false;
        }

        Republic other = (Republic)obj; // base.Equals уже проверил тип
        return PresidentName == other.PresidentName && TermYears == other.TermYears;
    }

    // Хэш по общим полям допустим: равные объекты всё равно получают равные хэши
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override object Clone()
    {
        return new Republic(this);
    }
}
