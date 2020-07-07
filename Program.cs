using System;
using System.Net;
using System.IO;
using System.Collections.Generic;

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

// *****************************************************************************
// *****************************************************************************
// *****************************************************************************

// INTERMEDIATE CHALLENGE INSTRUCTIONS:
/*
For the purpose of this challenge, a k-ary necklace of length n is a sequence of n letters 
chosen from k options, e.g. ABBEACEEA is a 5-ary necklace of length 9. 
Note that not every letter needs to appear in the necklace. 
Two necklaces are equal if you can move some letters from the beginning to the 
end to make the other one, otherwise maintaining the order. For instance, ABCDE is equal to DEABC. 
For more detail, see challenge #383 Easy: Necklace matching.

Today's challenge is, given k and n, find the number of distinct k-ary necklaces of length n. 
That is, the size of the largest set of k-ary necklaces of length n such that no two of them are 
equal to each other. You do not need to actually generate the necklaces, just count them.

For example, there are 24 distinct 3-ary necklaces of length 4, so 
necklaces(3, 4) is 24. Here they are:

AAAA  BBBB  CCCC
AAAB  BBBC  CCCA
AAAC  BBBA  CCCB
AABB  BBCC  CCAA
ABAB  BCBC  CACA
AABC  BBCA  CCAB
AACB  BBAC  CCBA
ABAC  BCBA  CACB
You only need to handle inputs such that kn < 10,000.

necklaces(2, 12) => 352
necklaces(3, 7) => 315
necklaces(9, 4) => 1665
necklaces(21, 3) => 3101
necklaces(99, 2) => 4950
The most straightforward way to count necklaces is to generate all kn patterns, 
and deduplicate them (potentially using your code from Easy #383). 
This is an acceptable approach for this challenge, as long as you can actually run your 
program through to completion for the above examples.

Optional optimization
A more efficient way is with the formula:

necklaces(k, n) = 1/n * Sum of (phi(a) k^b)
    for all positive integers a,b such that a*b = n.
For example, the ways to factor 10 into two positive integers are 1x10, 2x5, 5x2, and 10x1, so:

necklaces(3, 10)
    = 1/10 (phi(1) 3^10 + phi(2) 3^5 + phi(5) 3^2 + phi(10) 3^1)
    = 1/10 (1 * 59049 + 1 * 243 + 4 * 9 + 4 * 3)
    = 5934
phi(a) is Euler's totient function, which is the number of positive integers x less than or 
equal to a such that the greatest common divisor of x and a is 1. 
For instance, phi(12) = 4, because 1, 5, 7, and 11 are coprime with 12.

An efficient way to compute phi is with the formula:

phi(a) = a * Product of (p-1) / Product of (p)
    for all distinct prime p that divide evenly into a.
For example, for a = 12, the primes that divide a are 2 and 3. So:

phi(12) = 12 * ((2-1)*(3-1)) / (2*3) = 12 * 2 / 6 = 4
If you decide to go this route, you can test much bigger examples.

necklaces(3, 90) => 96977372978752360287715019917722911297222
necklaces(123, 18) => 2306850769218800390268044415272597042
necklaces(1234567, 6) => 590115108867910855092196771880677924
necklaces(12345678910, 3) => 627225458787209496560873442940
If your language doesn't easily let you handle such big numbers, that's okay. 
But your program should run much faster than O(kn).
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

            // this part tests the function that finds the four rotatable words in the word list
            // commenting it out as it takes forever to run
            /*
            string[] fourWords = FourMatchingWords();
            foreach (string s in fourWords)
            {
                Console.WriteLine(s);
            }
            */

            Console.WriteLine("INTERMEDIATE CHALLENGE RESULTS\n");
            //INTERMEDIATE CHALLENGE HERE:
            Console.WriteLine($"phi(1) = {Phi(1)} (should be 1)");
            Console.WriteLine($"phi(2) = {Phi(2)} (should be 1)");
            Console.WriteLine($"phi(9) = {Phi(9)} (should be 6)");
            Console.WriteLine($"phi(12) = {Phi(12)} (should be 4)");
            Console.WriteLine($"phi(10) = {Phi(10)} (should be 4)");
            Console.WriteLine($"phi(5) = {Phi(5)} (should be 4)");
            Console.WriteLine($"phi(3) = {Phi(3)} (should be 2)");
            Console.WriteLine();
            Console.WriteLine($"necklaces(3, 4) = {Necklaces(3, 4)} - (should be 24)");
            Console.WriteLine($"necklaces(2, 12) = {Necklaces(2, 12)} - (should be 352)");
            Console.WriteLine($"necklaces(3, 7) = {Necklaces(3, 7)} - (should be 315)");
            Console.WriteLine($"necklaces(9, 4) = {Necklaces(9, 4)} - (should be 1665)");
            Console.WriteLine($"necklaces(21, 3) = {Necklaces(21, 3)} - (should be 3101)");
            Console.WriteLine($"necklaces(99, 2) = {Necklaces(99, 2)} - (should be 4950)");
            Console.WriteLine($"necklaces(3, 10) = {Necklaces(3, 10)} - (should be 5934)");
            Console.WriteLine($"necklaces(12345678910, 3) = {Necklaces(12345678910, 3)} - (should be 627225458787209496560873442940)");
            Console.WriteLine();
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
        // This beautiful solution for the challenge is from someone on reddit:
        /*
        public static bool IsSame(string a, string b) => a.Length == b.Length ? (a + a).Contains(b) : false;
        */

        // BONUS 1 - this one tells you how many times a string is in a string (a = 1; aaa = 3; abc = 1; abab = 2)
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
        // BONUS 2 - this finds the four IsSame words in the enable1 word list (takes about 10 minutes to run)
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
        // *************************************************************************************
        // INTERMEDIATE CHALLENGE FUNCTIONS
        // main function for possible combos of k-ary necklaces of length n
        // necklaces(k, n) = 1/n * Sum of (phi(a) k^b) -- for all positive integers a,b such that a*b = n
        static double Necklaces(double k, double n)
        {
            List<double> divisibles = new List<double>();
            for (double d = 1; d <= n; d++)
            {
                if (n % d == 0)
                {
                    divisibles.Add(d);
                    divisibles.Add(n / d);
                }
            }

            double backSum = 0;
            // this starts at 0 index and calculates the back half of the equation
            for (int d = 0; d < divisibles.Count - 1; d += 2)
            {
                backSum += Phi(divisibles[d]) * Math.Pow(k, divisibles[d+1]);
            }
            return 1 / n * backSum;

        }
        // calculate phi
        // phi(a) = a * Product of (p-1) / Product of (p) -- for all distinct prime p that divide evenly into a.
        static double Phi(double input)
        {
            // first we need a list of all the primes AND can multiply into "input"
            List<double> primes = new List<double>();
            for (double d = 2; d <= input; d++)
            {
                if (IsPrime(d) && input % d == 0)
                {
                    primes.Add(d);
                }
            }
            // figure out numerator and denominator
            double numerator = input;
            double denominator = 1;
            foreach (double d in primes)
            {
                numerator *= d - 1;
                denominator *= d;
            }
            // calculate the result
            return numerator / denominator;
            
        }
        // calculate if a number is prime
        static bool IsPrime(double num)
        {
            if (num < 2) {return false;}
            for (double d = 2; d < num; d++)
            {
                if (num % d == 0)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
