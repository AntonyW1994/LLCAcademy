using System.Linq;
using System.Text.RegularExpressions;

namespace LLCAcademy.Helpers
{
    public static class GeneralHelper
    {
        public static string CleanPhoneNumber(string telephone)
        {
            if (string.IsNullOrEmpty(telephone))
                return string.Empty;

            var cleanedNumber = Regex.Replace(telephone, "[^0-9+]", string.Empty);
            cleanedNumber = Regex.Replace(cleanedNumber, "(?<=.)([+](?=.))", "");

            if (cleanedNumber.StartsWith("+44"))
            {
                cleanedNumber = cleanedNumber.Substring(3);
                cleanedNumber = "0" + cleanedNumber;
            }

            if (cleanedNumber.StartsWith("44"))
            {
                cleanedNumber = cleanedNumber.Substring(2);
                cleanedNumber = "0" + cleanedNumber;
            }

            return cleanedNumber;
        }

        public static string ConvertToTitleCase(string oldString)
        {
            oldString = oldString.ReplaceAt(0, oldString.Substring(0, 1).ToUpper().ToCharArray().FirstOrDefault());
            for (var i = 0; i < oldString.Length; i++)
            {
                if (oldString.Substring(i, 1) == " " && i < oldString.Length - 1)
                    oldString = oldString.ReplaceAt(i + 1, oldString.Substring(i + 1, 1).ToUpper().ToCharArray().FirstOrDefault());
            }
            return oldString;
        }

        public static string ReplaceAt(this string input, int index, char newChar)
        {
            var chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}