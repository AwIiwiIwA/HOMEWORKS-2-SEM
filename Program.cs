using System;
using System.Collections.Generic;
using NUnit.Framework;

public interface IStack<T>
{
    void Push(T item);
    T Pop();
    T Peek();
    bool IsEmpty();
    int Count { get; }
}

public class ArrayStack<T> : IStack<T>
{
    private T[] items;
    private int top;

    public ArrayStack(int capacity)
    {
        items = new T[capacity];
        top = -1;
    }

    public void Push(T item)
    {
        if (top == items.Length - 1)
        {
            throw new StackOverflowException("Стек полон");
        }

        top++;
        items[top] = item;
    }

    public T Pop()
    {
        if (top == -1)
        {
            throw new InvalidOperationException("Стек пуст");
        }

        T item = items[top];
        top--;
        return item;
    }

    public T Peek()
    {
        if (top == -1)
        {
            throw new InvalidOperationException("Стек пуст");
        }

        return items[top];
    }

    public bool IsEmpty()
    {
        return top == -1;
    }

    public int Count
    {
        get { return top + 1; }
    }
}

public class ListStack<T> : IStack<T>
{
    private List<T> items;

    public ListStack()
    {
        items = new List<T>();
    }

    public void Push(T item)
    {
        items.Add(item);
    }

    public T Pop()
    {
        if (items.Count == 0)
        {
            throw new InvalidOperationException("Стек пуст");
        }

        T item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }

    public T Peek()
    {
        if (items.Count == 0)
        {
            throw new InvalidOperationException("Стек пуст");
        }

        return items[items.Count - 1];
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }

    public int Count
    {
        get { return items.Count; }
    }
}

public class Calculator
{
    private IStack<double> stack;

    public Calculator(IStack<double> stack)
    {
        this.stack = stack;
    }

    public double Evaluate(string expression)
    {
        string[] tokens = expression.Split(' ');

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double number))
            {
                stack.Push(number);
            }
            else
            {
                double operand2 = stack.Pop();
                double operand1 = stack.Pop();

                switch (token)
                {
                    case "+":
                        stack.Push(operand1 + operand2);
                        break;
                    case "-":
                        stack.Push(operand1 - operand2);
                        break;
                    case "*":
                        stack.Push(operand1 * operand2);
                        break;
                    case "/":
                        if (operand2 == 0)
                        {
                            throw new DivideByZeroException("Деление на ноль запрещено");
                        }
                        stack.Push(operand1 / operand2);
                        break;
                    default:
                        throw new ArgumentException($"Некорректный токен: {token}");
                }
            }
        }

        if (stack.Count == 1)
        {
            return stack.Peek();
        }
        else
        {
            throw new ArgumentException("Некорректное выражение");
        }
    }
}
//TESTS
[TestFixture]
public class StackCalculatorTests
{
    private static IEnumerable<TestCaseData> Stacks
    {
        get
        {
            yield return new TestCaseData(new ArrayStack<double>(100));
            yield return new TestCaseData(new ListStack<double>());
        }
    }

    [TestCaseSource(nameof(Stacks))]
    public void Calculator_CorrectExpression_ReturnsCorrectResult(IStack<double> stack)
    {
        Calculator calculator = new Calculator(stack);
        var result = calculator.Evaluate("5 1 2 + 4 * + 3 -");
        Assert.AreEqual(14, result);
    }

    [TestCaseSource(nameof(Stacks))]
    public void Calculator_IncorrectExpression_ThrowsArgumentException(IStack<double> stack)
    {
        Calculator calculator = new Calculator(stack);
        Assert.Throws<ArgumentException>(() => calculator.Evaluate("5 1 2 + 4 * + 3 - $"));
    }

    [TestCaseSource(nameof(Stacks))]
    public void Calculator_DivisionByZero_ThrowsDivideByZeroException(IStack<double> stack)
    {
        Calculator calculator = new Calculator(stack);
        Assert.Throws<DivideByZeroException>(() => calculator.Evaluate("5 0 /"));
    }
}
//TESTS END

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        IStack<double> stack = new ArrayStack<double>(100);

        Calculator calculator = new Calculator(stack);

        Console.Write("Введите арифметическое выражение в постфиксной записи: ");
        string expression = Console.ReadLine();

        try
        {
            double result = calculator.Evaluate(expression);
            Console.WriteLine($"Результат: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
