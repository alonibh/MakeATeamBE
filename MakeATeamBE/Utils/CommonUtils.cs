using System;

namespace MakeATeamBE.Utils
{
    public static class CommonUtils
    {
        public static string CreateRandomTeamCode()
        {
            int length = 6;
            int randomValue = new Random().Next(0, 999999);
            return randomValue.ToString("D" + length.ToString());
        }
    }
}
