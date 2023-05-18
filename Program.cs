using System;
using System.Linq;
using System.Globalization;
using System.Threading;
public class Program
{
    public static void Main()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
        Console.WriteLine("Введите строку для преобразования Барроуза-Уилера:");
        string? input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Введена пустая строка. Завершение программы.");
            return;
        }

        var (output, position) = BWT(input);
        Console.WriteLine($"Преобразованная строка: {output}, позиция: {position}");

        string recovered = InverseBWT(output, position);
        Console.WriteLine($"Восстановленная строка: {recovered}");
    }

    public static (string, int) BWT(string s)
    {
        string[] rotations = new string[s.Length];
        for (int i = 0; i < s.Length; i++)
            rotations[i] = s.Substring(i) + s.Substring(0, i);

        Array.Sort(rotations);
        int position = Array.IndexOf(rotations, s);
        string result = string.Concat(rotations.Select(str => str[^1])); // Last character of each rotation

        return (result, position);
    }

    public static string InverseBWT(string r, int position)
    {
        int length = r.Length;
        int[] table = new int[length];
        int[] counts = new int[256];

        for (int i = 0; i < length; i++) counts[r[i]]++;
        for (int i = 1; i < 256; i++) counts[i] += counts[i - 1];
        for (int i = length - 1; i >= 0; i--) table[--counts[r[i]]] = i;

        char[] result = new char[length];
        for (int i = 0; i < length; i++)
        {
            position = table[position];
            result[i] = r[position];
        }

        return new string(result);
    }
}
