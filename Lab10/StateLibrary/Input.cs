using System;

namespace StateLibrary;

// Ввод с клавиатуры
public static class Input
{
    public static string ReadText(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? "";
    }

    public static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        // При неверном вводе вернётся 0, а свойство заменит его допустимым значением
        int.TryParse(Console.ReadLine(), out int value);
        return value;
    }
}
