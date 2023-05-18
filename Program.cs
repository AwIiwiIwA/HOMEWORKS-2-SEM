using System;
using System.Collections.Generic;
using NUnit.Framework;
using System;


public class MyList<T>
{
    protected List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public void Remove(T item)
    {
        if (!items.Contains(item))
        {
            throw new ElementDoesNotExistException();
        }
        items.Remove(item);
    }

    public void Update(int position, T item)
    {
        if (position < 0 || position >= items.Count)
        {
            throw new IndexOutOfRangeException();
        }
        items[position] = item;
    }
}

public class UniqueList<T> : MyList<T>
{
    public new void Add(T item)
    {
        if (items.Contains(item))
        {
            throw new ElementAlreadyExistsException();
        }
        base.Add(item);
    }

    public new void Update(int position, T item)
    {
        if (items.Contains(item))
        {
            throw new ElementAlreadyExistsException();
        }
        base.Update(position, item);
    }
}

public class ElementAlreadyExistsException : Exception
{
    public ElementAlreadyExistsException()
        : base("The element already exists in the list.")
    {
    }
}

public class ElementDoesNotExistException : Exception
{
    public ElementDoesNotExistException()
        : base("The element does not exist in the list.")
    {
    }
}
[TestFixture]
public class ListTests
{
    [Test]
    public void TestAddUniqueList()
    {
        var list = new UniqueList<int>();
        list.Add(1);
        Assert.Throws<ElementAlreadyExistsException>(() => list.Add(1));
    }

    [Test]
    public void TestRemoveUniqueList()
    {
        var list = new UniqueList<int>();
        list.Add(1);
        list.Remove(1);
        Assert.Throws<ElementDoesNotExistException>(() => list.Remove(1));
    }

    [Test]
    public void TestUpdateUniqueList()
    {
        var list = new UniqueList<int>();
        list.Add(1);
        list.Add(2);
        Assert.Throws<ElementAlreadyExistsException>(() => list.Update(1, 1));
    }

    [Test]
    public void TestAddMyList()
    {
        var list = new MyList<int>();
        list.Add(1);
        Assert.DoesNotThrow(() => list.Add(1));
    }

    [Test]
    public void TestRemoveMyList()
    {
        var list = new MyList<int>();
        list.Add(1);
        list.Remove(1);
        Assert.Throws<ElementDoesNotExistException>(() => list.Remove(1));
    }

    [Test]
    public void TestUpdateMyList()
    {
        var list = new MyList<int>();
        list.Add(1);
        list.Add(2);
        Assert.DoesNotThrow(() => list.Update(1, 1));
    }
}

class Program
{
    static void Main(string[] args)
    {
        var myList = new MyList<int>();
        myList.Add(1);
        myList.Add(2);
        myList.Add(3);
        Console.WriteLine(string.Join(", ", myList));  // Вывод: 1, 2, 3

        var uniqueList = new UniqueList<int>();
        uniqueList.Add(1);
        uniqueList.Add(2);
        try
        {
            uniqueList.Add(1);  // Попытка добавить уже существующий элемент
        }
        catch (ElementAlreadyExistsException ex)
        {
            Console.WriteLine(ex.Message);  // Вывод: The element already exists in the list.
        }
        Console.WriteLine(string.Join(", ", uniqueList));  // Вывод: 1, 2
    }
}
