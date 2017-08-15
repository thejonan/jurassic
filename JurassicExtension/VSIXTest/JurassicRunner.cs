using Jurassic;
using Jurassic.Library;

namespace JurassicExtensionTest
{
    public class JurassicRunner
    {
        private static void Main(string[] args)
        {
            ScriptEngine jurassic = new ScriptEngine();
            jurassic.SetGlobalValue("console", new FirebugConsole(jurassic));
            jurassic.EnableDebugging = true;
            jurassic.ExecuteFile("Script.js");
        }
    }
}
