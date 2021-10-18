
    using System;

    public static class StringHelper
    {
        public static string ConvertIntToHumanReadableString(int value)
        {
            return $"{value:n0}";
        }

        public static string ConvertIntToHumanReadableMoneyString(int value)
        {
            return $"${ConvertIntToHumanReadableString(value)}";
        }
    }
