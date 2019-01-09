using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution
{

    // Complete the freqQuery function below.
    static List<int> freqQuery(List<List<int>> queries)
    {
        List<int> ans = new List<int>();
        Dictionary<int, int> runningFrequencyCounts = new Dictionary<int, int>();
        Dictionary<int, int> freqCounts = new Dictionary<int, int>();
        foreach(var query in queries)
        {
            int command = query[0];
            int item = query[1];
            switch(command)
            {
                case 1:
                    runningFrequencyCounts.TryGetValue(item, out var existingValToInc);
                    freqCounts.TryGetValue(existingValToInc, out var existingFreqCountVal);
                    freqCounts[existingValToInc] = existingFreqCountVal > 0 ? existingFreqCountVal - 1 : 0;
                    runningFrequencyCounts[item] = existingValToInc + 1;
                    freqCounts.TryGetValue(existingValToInc + 1, out var existingFreqCountValToInc);
                    freqCounts[existingValToInc + 1] = existingFreqCountValToInc + 1;
                    break;
                case 2:
                    runningFrequencyCounts.TryGetValue(item, out var existingValToDec);
                    freqCounts.TryGetValue(existingValToDec, out var existingFreqCountVal2);
                    freqCounts[existingValToDec] = existingFreqCountVal2 > 0 ? existingFreqCountVal2 - 1 : 0;
                    runningFrequencyCounts[item] = existingValToDec > 0 ? existingValToDec - 1 : 0;
                    freqCounts.TryGetValue(existingValToDec - 1, out var existingFreqCountValToInc2);
                    freqCounts[existingValToDec - 1] = existingFreqCountValToInc2 + 1;
                    break;
                case 3:
                    //If any freq counts = item then return 1. Stop when found
                    freqCounts.TryGetValue(item, out var currentFreqCount);
                    bool found = currentFreqCount > 0;
                    ans.Add(found ? 1 : 0);
                    break;
                default:
                    Console.WriteLine("Should not receive any other command type. Error.");
                    break;
            }
        }
        return ans;

    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<int>> queries = new List<List<int>>();

        for (int i = 0; i < q; i++)
        {
            queries.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(queriesTemp => Convert.ToInt32(queriesTemp)).ToList());
        }

        List<int> ans = freqQuery(queries);

        textWriter.WriteLine(String.Join("\n", ans));

        textWriter.Flush();
        textWriter.Close();
    }
}
