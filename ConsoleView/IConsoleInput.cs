using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleView
{
    public interface IConsoleInput
    {
        int[] GetIntegers(string prompt, int numberCount, int min = int.MinValue, int max = int.MaxValue);
        string[] GetLineTokens(string prompt, string expectedInput, int tokenCount);
    }
}
