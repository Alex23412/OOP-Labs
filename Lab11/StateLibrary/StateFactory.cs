using System;

namespace StateLibrary;

// Создание объектов иерархии: заданного типа и со случайными значениями
public static class StateFactory
{
    private static readonly Type[] stateTypes = { typeof(State), typeof(Republic), typeof(Monarchy), typeof(Kingdom) };
    private static readonly string[] stateTypeNames = { "Государство", "Республика", "Монархия", "Королевство" };

    public static Type[] GetStateTypes()
    {
        return (Type[])stateTypes.Clone();
    }

    public static string[] GetStateTypeNames()
    {
        return (string[])stateTypeNames.Clone();
    }

    // Объект заданного типа со значениями по умолчанию (конструктор без параметров)
    public static State CreateState(Type stateType)
    {
        ArgumentNullException.ThrowIfNull(stateType);
        if (!typeof(State).IsAssignableFrom(stateType))
        {
            throw new ArgumentException("Тип должен принадлежать иерархии State.", nameof(stateType));
        }
        return (State)Activator.CreateInstance(stateType);
    }

    public static State CreateRandomState(Type stateType)
    {
        State state = CreateState(stateType);
        state.RandomInit();
        return state;
    }

    // Случайное государство случайного типа иерархии
    public static State CreateRandomState()
    {
        return CreateRandomState(stateTypes[RandomData.GetInt(0, stateTypes.Length - 1)]);
    }

    // Массив случайных объектов; типы чередуются, поэтому при count >= 4 есть объекты всех типов
    public static State[] CreateRandomStates(int count)
    {
        var states = new State[count];
        for (int i = 0; i < count; i++)
        {
            states[i] = CreateRandomState(stateTypes[i % stateTypes.Length]);
        }
        return states;
    }
}