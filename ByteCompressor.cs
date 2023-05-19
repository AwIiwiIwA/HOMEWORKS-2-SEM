using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class ByteCompressor
{
    // Этот код сжимает заданный массив байтов, подсчитывая количество последовательных байтов, которые совпадают, а затем добавляя количество и байт в новый список.
    public static (byte[], double) Compress(byte[] data)
    {
        // Создаем новый список для хранения сжатых данных
        var compressedData = new List<byte>();
        // Проходим по заданным данным
        for (int i = 0; i < data.Length;)
        {
            // Считаем количество последовательных одинаковых байтов
            int count = 1;
            while (i + count < data.Length && data[i] == data[i + count])
            {
                count++;
            }
            // Добавляем количество и байт в новый список
            compressedData.Add((byte)count);
            compressedData.Add(data[i]);
            // Увеличиваем счетчик цикла
            i += count;
        }
        // Вычисляем коэффициент сжатия
        double ratio = (double)compressedData.Count / data.Length;

        // Возвращаем сжатые данные и коэффициент сжатия
        return (compressedData.ToArray(), ratio);
    }

    // Этот код разархивирует массив байтов.
    // Он создает новый список байтов для хранения разархивированных данных.
    // Затем он проходит через сжатые данные парами по два байта.
    // Первый байт из пары указывает, сколько раз второй байт должен быть добавлен в список разархивированных данных.
    public static byte[] Decompress(byte[] compressedData)
    {
        var decompressedData = new List<byte>();
        for (int i = 0; i < compressedData.Length; i += 2)
        {
            for (int j = 0; j < compressedData[i]; j++)
            {
                decompressedData.Add(compressedData[i + 1]);
            }
        }

        return decompressedData.ToArray();
    }

    // Здесь мы вызываем метод CalculateCompressionRatio для вычисления коэффициента сжатия путем деления длины сжатых данных на длину исходных данных.
    public static double CalculateCompressionRatio(byte[] compressedData, byte[] originalData)
    {
        return (double)compressedData.Length / originalData.Length;
    }
}

[TestFixture]
public class ByteCompressorTests
{
    [Test]
    public void TestCompressionDecompression()
    {
        byte[] data = { 1, 1, 1, 2, 2, 3, 3, 3, 3 };
        var (compressedData, ratio) = ByteCompressor.Compress(data);
        Assert.Less(ratio, 1.0);
        byte[] decompressedData = ByteCompressor.Decompress(compressedData);
        Assert.AreEqual(data, decompressedData);
    }
}

//Добавил Main с тестами выше и вывод
public class Program
{
    public static void Main()
    {
        byte[] data = { 1, 1, 1, 1, 1, 1, 1, 1, 1};
        var (compressedData, ratio) = ByteCompressor.Compress(data);
        Assert.Less(ratio, 1.0);
        byte[] decompressedData = ByteCompressor.Decompress(compressedData);
        Assert.AreEqual(data, decompressedData);
        Console.WriteLine($"Compression Ratio: {ratio}");
        Console.WriteLine("Decompressed Data: " + string.Join(", ", decompressedData));
    }
}
