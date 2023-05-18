public class Trie
{
    private readonly Dictionary<char, Trie> children = new Dictionary<char, Trie>();
    private bool endOfWord;
    public int Size { get; private set; }

    public bool Add(string element)
    {
        Trie node = this;
        foreach (char c in element)
        {
            if (!node.children.ContainsKey(c))
            {
                node.children.Add(c, new Trie());
            }
            node = node.children[c];
        }
        if (node.endOfWord)
        {
            return false;
        }
        node.endOfWord = true;
        node = this;
        foreach (char c in element)
        {
            node.Size++;
            node = node.children[c];
        }
        return true;
    }

    public bool Contains(string element)
    {
        Trie node = this;
        foreach (char c in element)
        {
            if (!node.children.ContainsKey(c))
            {
                return false;
            }
            node = node.children[c];
        }
        return node.endOfWord;
    }

    public bool Remove(string element)
    {
        Trie node = this;
        var stack = new Stack<(char, Trie)>();
        foreach (char c in element)
        {
            if (!node.children.ContainsKey(c))
            {
                return false;
            }
            stack.Push((c, node));
            node = node.children[c];
        }
        if (!node.endOfWord)
        {
            return false;
        }
        node.endOfWord = false;
        node = this;
        foreach (char c in element)
        {
            node.Size--;
            node = node.children[c];
        }
        while (node != this && !node.endOfWord && node.children.Count == 0)
        {
            (char c, Trie parent) = stack.Pop();
            parent.children.Remove(c);
            node = parent;
        }
        return true;
    }

    public int HowManyStartsWithPrefix(string prefix)
    {
        Trie node = this;
        foreach (char c in prefix)
        {
            if (!node.children.ContainsKey(c))
            {
                return 0;
            }
            node = node.children[c];
        }
        return node.Size;
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
