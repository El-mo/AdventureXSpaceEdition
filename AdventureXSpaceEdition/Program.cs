using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureXSpaceEdition
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveFile saveFile = new SaveFile("save.txt");
            string playerName = "";
            
            Console.WriteLine("What is your name?");
            Console.Write(playerName);
            playerName = Console.ReadLine();
            
        }
    }
}
