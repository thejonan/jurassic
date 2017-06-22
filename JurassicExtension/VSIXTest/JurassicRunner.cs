using System;
using Jurassic;
using System.IO;

namespace JurassicExtensionTest
{
    public class JurassicRunner
    {
        private static void Main(string[] args)
        {
            ScriptEngine jurassic = new ScriptEngine();
            jurassic.EnableDebugging = true;
            jurassic.ExecuteFile("Script.js");
        }
    }
}

