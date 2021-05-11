using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace pyramid
{
    class Program
    {
        public static Token head;

        public static bool asal(int n)
        {
            if (n <= 1)
                return false;


            for (int i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        static void Main(string[] args)
        {
            //reading file...
            string readText = File.ReadAllText("pyramid.txt");

            //Tokenizing...
            head = tokenize(readText);

            //finding path...
            var list = head.iterate();

            //writing down the results;
            Console.Write("The Path: ");
            foreach (var l in list) Console.Write(l.sayi + " ");
            Console.WriteLine("\nThe Summary: " + Token.sum(list));
            Console.ReadLine();
        }

        //tokenizing all numbers from text;
        public static Token tokenize(string readText)
        {
            string[] satirlar = readText.Split('\n');
            List<Token> ust = new List<Token>();
            List<Token> alt = new List<Token>();
            for (int i = satirlar.Length - 1; i >= 0; i--)
            {
                string[] sayilar = satirlar[i].Split(' ');
                for (int j = 0; j < sayilar.Length; j++)
                {
                    Token t = new Token(sayilar[j]);
                    if (alt.Count != 0)
                    {
                        t.left = alt[j];
                        t.right = alt[j + 1];
                    }
                    else
                    {
                        t.left = null;
                        t.right = null;
                    }

                    ust.Add(t);
                }
                alt = ust;
                ust = new List<Token>();
            }
            return alt[0];
        }

        //we tokenize numbers and provide tree form.
        public class Token
        {
            public int sayi;
            public Token left;
            public Token right;
            public Token(int sayi)
            {
                this.sayi = sayi;
            }
            public Token(string sayi)
            {
                this.sayi = Int32.Parse(sayi);
            }

            //function that returns path that fits the rule and has maximum summary.
            public List<Token> iterate()
            {
                List<Token> list = new List<Token> { this };
                if (this.left == null || this.right == null) return list;

                List<Token> leftToken, rightToken;

                if (!asal(this.left.sayi)) leftToken = this.left.iterate();
                else leftToken = null; 

                if (!asal(this.right.sayi)) rightToken = this.right.iterate();
                else rightToken = null;

                if (leftToken == null && rightToken == null) return null;
                if(leftToken == null)
                {
                    list.AddRange(rightToken);
                    return list;
                }
                if (rightToken == null)
                {
                    list.AddRange(leftToken);
                    return list;
                }
                
                if (sum(leftToken) == 0 && sum(rightToken) == 0) return null;

                if (sum(leftToken) > sum(rightToken))
                {
                    list.AddRange(leftToken);
                    return list;
                }
                else
                {
                    list.AddRange(rightToken);
                    return list;
                }
            }

            //function that gives the summary of given list.
            public static int sum(List<Token> liste)
            {
                var top = 0;
                foreach (var l in liste) top += l.sayi;
                return top;
            }
        }

        
    }
}
