using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WordGame
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.InputEncoding = System.Text.Encoding.UTF8;
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            var wordsCallen = new Stack<string>();

            while (true)
            {
                try
                {
                    Console.WriteLine("Въведи дума.");
                    Console.WriteLine("Имаш 1 минута");
                    var word = Reader.ReadLine(60000);

                    if (wordsCallen.Count > 0)
                    {
                        if (wordsCallen.Peek()[wordsCallen.Peek().Length - 1] != word[0])
                        {
                            Console.WriteLine();
                            Console.WriteLine(new string('`', 37));
                            Console.WriteLine("Думата не отговаря на изискването!");
                            Console.WriteLine();
                            Console.WriteLine(new string('`', 37));
                            Console.WriteLine();
                            continue;
                        }
                    }

                    if (wordsCallen.Contains(word))
                    {
                        Console.WriteLine();
                        Console.WriteLine(new string('*', 17));
                        Console.WriteLine("Думата е казана опитай пак!");
                        Console.WriteLine();
                        Console.WriteLine(new string('*', 17));
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(new string('$', 17));
                        Console.WriteLine("Приема се!");
                        wordsCallen.Push(word);
                        Console.WriteLine(new string('$', 17));
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(new string('-', 20));
                    Console.WriteLine("Времето ти изтече!");
                    Console.WriteLine("Секунда е много :))");
                    Console.WriteLine(new string('-', 20));
                    Console.WriteLine();
                    continue;
                }
            }
        }
    }

    class Reader
    {
        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static string input;

        static Reader()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(reader);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private static void reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = Console.ReadLine();
                gotInput.Set();
            }
        }

        // omit the parameter to read a line without a timeout
        public static string ReadLine(int timeOutMillisecs = Timeout.Infinite)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return input;
            else
                throw new TimeoutException("User did not provide input within the timelimit.");
        }
    }
}
