using HW01_2024.Interfaces;
using HW01_2024.Logic;

namespace HW01_2024
{
    class Program
    {
        static void Main()
        {
            IGame game = new Game();
            game.Start();
        }
    }
}