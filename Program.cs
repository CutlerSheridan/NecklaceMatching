﻿using System;
using System.Net;
using System.IO;

namespace NecklaceMatching
{
    /*
    [2020-03-09] Challenge #383 [Easy] Necklace matching
Challenge
Imagine a necklace with lettered beads that can slide along the string. 
Here's an example image. In this example, you could take the N off NICOLE and slide it around 
to the other end to make ICOLEN. Do it again to get COLENI, and so on. 
For the purpose of today's challenge, we'll say that the strings "nicole", "icolen", and "coleni" 
describe the same necklace.

Generally, two strings describe the same necklace if you can remove some number of letters 
from the beginning of one, attach them to the end in their original ordering, 
and get the other string. Reordering the letters in some other way does not, in general, 
produce a string that describes the same necklace.

Write a function that returns whether two strings describe the same necklace.

Examples
same_necklace("nicole", "icolen") => true
same_necklace("nicole", "lenico") => true
same_necklace("nicole", "coneli") => false
same_necklace("aabaaaaabaab", "aabaabaabaaa") => true
same_necklace("abc", "cba") => false
same_necklace("xxyyy", "xxxyy") => false
same_necklace("xyxxz", "xxyxz") => false
same_necklace("x", "x") => true
same_necklace("x", "xx") => false
same_necklace("x", "") => false
same_necklace("", "") => true

Optional Bonus 1
If you have a string of N letters and you move each letter one at a time from the start to the end, 
you'll eventually get back to the string you started with, after N steps. 
Sometimes, you'll see the same string you started with before N steps. 
For instance, if you start with "abcabcabc", you'll see the same string ("abcabcabc") 3 times over 
the course of moving a letter 9 times.

Write a function that returns the number of times you encounter the same starting string if 
you move each letter in the string from the start to the end, one at a time.

repeats("abc") => 1
repeats("abcabcabc") => 3
repeats("abcabcabcx") => 1
repeats("aaaaaa") => 6
repeats("a") => 1
repeats("") => 1

Optional Bonus 2
There is exactly one set of four words in the enable1 word list that all describe the 
same necklace. Find the four words.
    */
    class Program
    {
        static void Main(string[] args)
        {
            string input1 = "TEST";
            string input2 = "TEST";
            Console.WriteLine($"Input: \"{input1}\", \"{input2}\"\nOutput: {IsSame(input1, input2)}\n");
            input1 = "TEST";
            input2 = "ESTT";
            Console.WriteLine($"Input: \"{input1}\", \"{input2}\"\nOutput: {IsSame(input1, input2)}\n");
            input1 = "TEST";
            input2 = "TSET";
            Console.WriteLine($"Input: \"{input1}\", \"{input2}\"\nOutput: {IsSame(input1, input2)}\n");
            input1 = "ABCDEFGHIJ";
            input2 = "EFGHIJABCD";
            Console.WriteLine($"Input: \"{input1}\", \"{input2}\"\nOutput: {IsSame(input1, input2)}\n");
            input1 = "ABCDEFGHIJ";
            input2 = "ABCDEFGHJI";
            Console.WriteLine($"Input: \"{input1}\", \"{input2}\"\nOutput: {IsSame(input1, input2)}\n");
            input1 = "x";
            input2 = "x";
            Console.WriteLine($"Input: \"{input1}\", \"{input2}\"\nOutput: {IsSame(input1, input2)}\n");
            input1 = "xxyyy";
            input2 = "xxxyy";
            Console.WriteLine($"Input: \"{input1}\", \"{input2}\"\nOutput: {IsSame(input1, input2)}\n");
            input1 = "Xxy";
            input2 = "Yxx";
            Console.WriteLine($"Input: \"{input1}\", \"{input2}\"\nOutput: {IsSame(input1, input2)}\n");
            input1 = "";
            input2 = "";
            Console.WriteLine($"Input: \"{input1}\", \"{input2}\"\nOutput: {IsSame(input1, input2)}\n");
            input1 = "x";
            input2 = "xx";
            Console.WriteLine($"Input: \"{input1}\", \"{input2}\"\nOutput: {IsSame(input1, input2)}\n");
            Console.WriteLine("METHOD FOR REPEATS\n");
            input1 = "abc";
            Console.WriteLine($"Input: \"{input1}\"\nRepeats: {Repeats(input1)}\n");
            input1 = "a";
            Console.WriteLine($"Input: \"{input1}\"\nRepeats: {Repeats(input1)}\n");
            input1 = "aaa";
            Console.WriteLine($"Input: \"{input1}\"\nRepeats: {Repeats(input1)}\n");
            input1 = "abab";
            Console.WriteLine($"Input: \"{input1}\"\nRepeats: {Repeats(input1)}\n");
            input1 = "";
            Console.WriteLine($"Input: \"{input1}\"\nRepeats: {Repeats(input1)}\n");
            input1 = "aaaaaaaaaa";
            Console.WriteLine($"Input: \"{input1}\"\nRepeats: {Repeats(input1)}\n");

            string[] fourWords = FourMatchingWords();
            foreach (string s in fourWords)
            {
                Console.WriteLine(s);
            }
        }

        // METHODS
        // this one tells you if two necklaces match
        static bool IsSame(string n1, string n2)
        {
            if (n1.Length != n2.Length)
            {
                return false;
            }
            n1 = n1.ToLower();
            n2 = n2.ToLower();
            if (n1 == n2)
            {
                return true;
            }
            for (int i = 0; i < n1.Length; i++)
            {
                n2 = Rotate(n2);
                if (n1 == n2) {return true;}
            }
            return false;

        }
        // this rotates the necklace by one character and loops it back around
        static string Rotate(string necklace)
        {
            char charToMove = necklace[0];
            necklace = necklace.Remove(0, 1);
            return necklace.Insert(necklace.Length, charToMove.ToString());
        }
        // this one tells you how many times a string is in a string (a = 1; aaa = 3; abc = 1; abab = 2)
        static int Repeats(string n1)
        {
            n1 = n1.ToLower();
            string n2 = n1;
            int reps = 0;
            for (int i = 0; i < n1.Length; i++)
            {
                n2 = Rotate(n2);
                if (n1 == n2) {reps++;}
            }
            if (n1 == "") {reps = 1;}
            return reps;
        }
        // this finds the four IsSame words in the enable1 word list (takes about 10 minutes to run)
        static string[] FourMatchingWords()
        {
            string url = "https://raw.githubusercontent.com/dolph/dictionary/master/enable1.txt";
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(url);
            StreamReader reader = new StreamReader(stream);
            string[] words = reader.ReadToEnd().Split("\n");
            int counter;
            string[] fourWords = new string[4];
            for (int i = 0; i < words.Length; i++)
            {
                counter = 0;
                for (int n = 0; n < words.Length; n++)
                {
                    if (words[i].Length == words[n].Length)
                    {
                        if (IsSame(words[i], words[n]))
                        {
                            counter++;
                            fourWords[counter - 1] = words[n];
                        }
                    }
                    if (counter == 4)
                    {
                        return fourWords;
                    }
                }
            }
            return fourWords;
        }
    }
}
