#region Using Statements
using FireEmblem;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace FireEmblem

{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new GameApplication())
                game.Run();
        }
    }
#endif
}
