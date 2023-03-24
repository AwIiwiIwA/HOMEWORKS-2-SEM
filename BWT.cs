using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;

class BurrowWheelerTransformation
{      
    static string EncodeBWT(string input)
    {
        int lenText = input.Length;
        string[] rotations = new string[lenText];
        for (int i = 0; i < lenText; i++)
        {
            rotations[i] = input.Substring(i) + input.Substring(0, i);
        }
        Array.Sort(rotations);
        string bwt = "";
        for (int i = 0; i < lenText; i++)
        {
            bwt += rotations[i][lenText - 1];
        }
        bwt += "$";
        return bwt;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Write something to Burrows – Wheeler  Transformation: ");
        string input = Console.ReadLine();
        string bwt = EncodeBWT(input);

        Console.WriteLine($"Input text : {input}");

        Console.WriteLine($"Burrows-Wheeler Transform : {bwt}");
    }
}
