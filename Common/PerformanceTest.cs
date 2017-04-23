using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Common
{
    public static class PerformanceTest
    {
        public static void Run(string testName, params Func<string>[] tests)
        {
            Console.WriteLine($"Run test {testName}");
            var results = new Dictionary<string, long>();
            var stopwatch = new Stopwatch();
            foreach (var test in tests)
            {
                stopwatch.Restart();
                var resultName = test();
                stopwatch.Stop();
                var parrots = stopwatch.ElapsedMilliseconds;

                Console.WriteLine($"  - {resultName}:      {parrots.ToString().PadLeft(4)}ms");
                results.Add(resultName, parrots);
            }
            //var max = results.Max(x => x.Value);
            //var min = res
            Console.WriteLine($"  - {testName} Complete");
            Console.WriteLine();
        }


    }
}
