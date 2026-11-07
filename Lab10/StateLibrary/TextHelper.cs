using System;

namespace StateLibrary;

// Общая проверка текстовых свойств: пустая строка заменяется значением по умолчанию
public static class TextHelper
{
    public const string DefaultText = "Не указано";

    public static string GetTextOrDefault(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? DefaultText : value.Trim();
    }
}
