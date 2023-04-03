using System.Collections.Generic;
public class Trie
{
    private readonly Dictionary<char, Trie> _children = new Dictionary<char, Trie>();
    private bool _isEndOfWord;
    public bool Add(string element)
    {
        Trie node = this;
        foreach (char c in element)
        {
            if (!node._children.ContainsKey(c))
            {
                node._children.Add(c, new Trie());
            }
            node = node._children[c];
        }
        if (node._isEndOfWord)
        {
            return false;
        }
        node._isEndOfWord = true;
        return true;
    }
    public bool Contains(string element)
    {
        Trie node = this;
        foreach (char c in element)
        {
            if (!node._children.ContainsKey(c))
            {
                return false;
            }
            node = node._children[c];
        }
        return node._isEndOfWord;
    }
    public bool Remove(string element)
    {
        Trie node = this;
        var stack = new Stack<(char, Trie)>();
        foreach (char c in element)
        {
            if (!node._children.ContainsKey(c))
            {
                return false;
            }
            stack.Push((c, node));
            node = node._children[c];
        }
        if (!node._isEndOfWord)
        {
            return false;
        }
        node._isEndOfWord = false;
        while (node != this && !node._isEndOfWord && node._children.Count == 0)
        {
            (char c, Trie parent) = stack.Pop();
            parent._children.Remove(c);
            node = parent;
        }
        return true;
    }
    public int HowManyStartsWithPrefix(string prefix)
    {
        Trie node = this;
        foreach (char c in prefix)
        {
            if (!node._children.ContainsKey(c))
            {
                return 0;
            }
            node = node._children[c];
        }
        return CountWords(node);
    }
    public int Size { get; private set; }
    private int CountWords(Trie node)
    {
        int count = node._isEndOfWord ? 1 : 0;
        foreach (Trie child in node._children.Values)
        {
            count += CountWords(child);
        }
        return count;
    }
}
class Program
{
    static void Main(string[] args)
    {
        Trie trie = new Trie();
        Console.WriteLine(trie.Add("hello")); // true
        Console.WriteLine(trie.Add("world")); // true
        Console.WriteLine(trie.Add("hello")); // false
        Console.WriteLine(trie.Contains("world")); // true
        Console.WriteLine(trie.Contains("hello")); // true
        Console.WriteLine(trie.Contains("hi")); // false
        Console.WriteLine(trie.HowManyStartsWithPrefix("he")); // 1
        Console.WriteLine(trie.HowManyStartsWithPrefix("w")); // 1
        Console.WriteLine(trie.HowManyStartsWithPrefix("h")); // 1
        Console.WriteLine(trie.HowManyStartsWithPrefix("q")); // 0
        Console.WriteLine(trie.Remove("hello")); // true
        Console.WriteLine(trie.Contains("hello")); // false
        Console.WriteLine(trie.HowManyStartsWithPrefix("he")); // 0
        Console.WriteLine(trie.HowManyStartsWithPrefix("w")); // 1
        Console.WriteLine(trie.HowManyStartsWithPrefix("h")); // 0
        Console.WriteLine(trie.HowManyStartsWithPrefix("q")); // 0
    }
}