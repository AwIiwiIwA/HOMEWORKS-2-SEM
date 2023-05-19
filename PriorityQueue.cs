using System;
using System.Collections.Generic;
using NUnit.Framework;

public class PriorityQueue<T>
{
    private SortedSet<(int priority, int index, T value)> queue;
    private int count;

    public PriorityQueue()
    {
        // Создаём новый отсортированный набор для хранения приоритетной очереди.
        queue = new SortedSet<(int priority, int index, T value)>();
        count = 0;
    }

    // Добавляет элемент в приоритетную очередь с указанным приоритетом.
    // "value"-Значение элемента.
    // "priority"-Приоритет элемента (чем ниже значение, тем выше приоритет).
    public void Enqueue(T value, int priority)
    {
        queue.Add((-priority, count, value)); // "-priority" т.к. SortedSet сортирует по возрастанию
        count++;
    }

    // Извлекает элемент с наивысшим приоритетом из очереди и удаляет его.
    // Извлеченный элемент с наивысшим приоритетом.
    // "InvalidOperationException"-Выбрасывается, если очередь пуста.
    public T Dequeue()
    {
        if (queue.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }
        var item = queue.Min;
        queue.Remove(item);

        return item.value;
    }

    // Возвращает значение, указывающее, пуста ли очередь.
    public bool Empty => queue.Count == 0;
}

[TestFixture]
public class PriorityQueueTests
{
    [Test]
    public void TestPriorityQueue()
    {
        var queue = new PriorityQueue<string>();
        queue.Enqueue("Hello", 2);
        queue.Enqueue("World", 1);
        queue.Enqueue("!", 3);

        Assert.AreEqual("!", queue.Dequeue());
        Assert.AreEqual("Hello", queue.Dequeue());
        Assert.AreEqual("World", queue.Dequeue());
        Assert.IsTrue(queue.Empty);
    }
    [Test]
    public void TestPriorityQueue_ThrowsException_OnDequeueEmpty()
    {
        var queue = new PriorityQueue<string>();
        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }
}

//Добавил мейн для моментальной проверки тестами =))
public class Program
{
    public static void Main(string[] args)
    {
        //При добавлении элементов с приоритетами 2, 1 и 3, они будут упорядочены как 1, 2, 3.
        //При извлечении элементов из очереди с использованием метода Dequeue(), элемент с наивысшим приоритетом (3) будет извлечен первым, затем элементы с приоритетами 2 и 1.
        var queue = new PriorityQueue<string>();
        queue.Enqueue("Hello", 2);
        queue.Enqueue("World", 1);
        queue.Enqueue("=)", 3);
        queue.Enqueue("!", 3);
        Console.WriteLine("Queue items:");

        while (!queue.Empty)
        {
            string value = queue.Dequeue();
            Console.WriteLine(value);
        }
        Console.WriteLine("\nQueue is empty.");
        Assert.IsTrue(queue.Empty);
    }
}
