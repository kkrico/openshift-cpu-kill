﻿using System;
using System.Diagnostics;
using System.Threading;

namespace cpu_kill
{
    class Program
    {
        static void Main(string[] args)
        {
            int percentage = Int32.Parse(Environment.GetEnvironmentVariable("PORCENTAGEM") ?? "80");
            Console.Out.WriteLine($"Processadores: {Environment.ProcessorCount}");
            Console.Out.WriteLine($"Porcentagem: {percentage}");
            
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                (new Thread(() =>
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    while (true)
                    {
                        // Make the loop go on for "percentage" milliseconds then sleep the 
                        // remaining percentage milliseconds. So 40% utilization means work 40ms and sleep 60ms
                        if (watch.ElapsedMilliseconds > percentage)
                        {
                            Thread.Sleep(100 - percentage);
                            watch.Reset();
                            watch.Start();
                        }
                    }
                })).Start();
            }
        }
    }
}