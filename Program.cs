#nullable enable

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Router
{
    public int Id { get; set; }
    public List<Edge> Edges { get; set; } = new List<Edge>();
}

public class Edge
{
    public Router? From { get; set; }
    public Router? To { get; set; }
    public int Weight { get; set; }
}

public class Program
{
    static List<Router> routers = new List<Router>();

    static void Main(string[] args)
    {
        string inputFile = args[0];
        string outputFile = args[1];

        // Загрузить граф из файла
        LoadGraph(inputFile);

        // Проверить связность графа
        if (!IsGraphConnected())
        {
            Console.Error.WriteLine("Сеть не связна");
            Environment.Exit(1);
        }

        // Найти минимальное остовное дерево с максимальными весами
        var result = KruskalAlgorithm();

        // Записать результат в файл
        WriteResult(outputFile, result);
    }

    public static void LoadGraph(string path)
    {
        var lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            var parts = line.Split(':');
            var id = int.Parse(parts[0]);
            var router = routers.FirstOrDefault(r => r.Id == id) ?? new Router { Id = id };
            if (!routers.Contains(router))
                routers.Add(router);

            var edges = parts[1].Split(',');
            foreach (var edge in edges)
            {
                var edgeParts = edge.Split(' ');
                id = int.Parse(edgeParts[1]);
                var toRouter = routers.FirstOrDefault(r => r.Id == id) ?? new Router { Id = id };
                if (!routers.Contains(toRouter))
                    routers.Add(toRouter);

                var weight = int.Parse(edgeParts[2].Trim('(', ')'));
                router.Edges.Add(new Edge { From = router, To = toRouter, Weight = weight });
            }
        }
    }

    public static bool IsGraphConnected()
    {
        if (routers.Count == 0)
        {
            return false;
        }

        HashSet<int> visited = new HashSet<int>();
        Stack<Router> stack = new Stack<Router>();
        stack.Push(routers[0]);

        while (stack.Count > 0)
        {
            Router currentRouter = stack.Pop();
            visited.Add(currentRouter.Id);

            foreach (Edge edge in currentRouter.Edges)
            {
                if (!visited.Contains(edge.To.Id))
                {
                    stack.Push(edge.To);
                }
            }
        }

        return visited.Count == routers.Count;
    }


    public static List<Edge> KruskalAlgorithm()
    {
        var edges = routers.SelectMany(r => r.Edges).ToList();
        edges.Sort((e1, e2) => e2.Weight.CompareTo(e1.Weight)); // сортировка по убыванию
        var result = new List<Edge>();
        var set = new UnionFind(routers.Count);
        foreach (var edge in edges)
        {
            if (set.Union(edge.From.Id, edge.To.Id))
            {
                result.Add(edge);
            }
        }
        return result;
    }

    static void WriteResult(string path, List<Edge> edges)
    {
        using (var sw = new StreamWriter(path))
        {
            foreach (var edge in edges)
            {
                sw.WriteLine($"{edge.From.Id}: {edge.To.Id} ({edge.Weight})");
            }
        }
    }
}

public class UnionFind
{
    private int[] parent;
    private int[] rank;

    public UnionFind(int size)
    {
        parent = new int[size + 1];
        rank = new int[size + 1];
        for (int i = 0; i <= size; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }
    }

    public int Find(int node)
    {
        if (parent[node] != node)
            parent[node] = Find(parent[node]);
        return parent[node];
    }

    public bool Union(int node1, int node2)
    {
        int root1 = Find(node1);
        int root2 = Find(node2);
        if (root1 == root2)
            return false;
        if (rank[root1] < rank[root2])
            parent[root1] = root2;
        else if (rank[root1] > rank[root2])
            parent[root2] = root1;
        else
        {
            parent[root2] = root1;
            rank[root1]++;
        }
        return true;
    }
}
    