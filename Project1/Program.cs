using System;

namespace Project1
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Ranger86Game())
                game.Run();
        }
    }
}
