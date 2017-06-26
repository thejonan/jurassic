using Jurassic;

namespace JurassicExtensionTest
{
    public class JurassicRunner
    {
        private static void Main(string[] args)
        {
            ScriptEngine jurassic = new ScriptEngine();
            jurassic.EnableDebugging = true;
//            jurassic.EnableILAnalysis = true;
            jurassic.ExecuteFile("Script.js");
        }
    }
}

