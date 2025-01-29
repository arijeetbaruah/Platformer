using System;
using System.Reflection;
using UnityEngine;

namespace BP.Console
{
    public partial class ConsoleCommand
    {
        private DevConsole console;
        
        void Log(string msg) => console.Log(msg);
        void LogError(string msg) => console.LogError(msg);
        void LogSuccess(string msg) => console.LogSuccess(msg);
        
        public ConsoleCommand(DevConsole console)
        {
            this.console = console;
        }

        public void help()
        {
            var type = typeof(ConsoleCommand);
            int count = 1;
            foreach (var method in type.GetMethods(BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public))
            {
                if (method.ReturnType == typeof(void))
                {
                    Log($"{count}. {method.Name}");
                    count++;
                }
            }
        }

        public void test(int s, string n)
        {
            LogSuccess($"Testing {s}: {n}");
        }
    }
}
