using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace HelloUnoNdc.Skia.Tizen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new HelloUnoNdc.App(), args);
            host.Run();
        }
    }
}
