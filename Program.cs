using System;

class InsertionSort
{
    static void Main()
    {
        Console.WriteLine("Enter the number of elements: ");
        int n = Convert.ToInt32(Console.ReadLine()); //make convert numbers to 32bit 
        int[] arr = new int[n];

        Console.WriteLine("Enter the elements (write element and press Enter): ");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        InsertionSortAlgoritm(arr);

        Console.WriteLine("Sorted elements: ");
        foreach (int num in arr)
        {
            Console.Write("{0} ", num);
        }
    }

    static void InsertionSortAlgoritm(int[] arr)
    {
        int n = arr.Length;
        for (int i = 1; i < n; i++)
        {
            int key = arr[i];
            int j = i - 1;
            while (j >= 0 && arr[j] > key)
            {
                arr[j+1] = arr[j];
                j = j - 1;
            }
            arr[j+1] = key;
        }
    }
}