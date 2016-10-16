using System;

namespace AdventureXSpaceEdition
{
    public class Roll
    {
        public static bool IsSuccessful(int chance)
        {
            Random rand = new Random();
            return rand.Next(1, 100) <= chance;
        }
    }
}