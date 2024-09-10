using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal static class Translit
    {
        public static string Transliting(string text)
        {
            var translitMapping = new[]
            {
            new[] {"А", "A"}, new[] {"Б", "B"}, new[] {"В", "V"}, new[] {"Г", "G"},
            new[] {"Д", "D"}, new[] {"Е", "E"}, new[] {"Ё", "YO"}, new[] {"Ж", "ZH"},
            new[] {"З", "Z"}, new[] {"И", "I"}, new[] {"Й", "Y"}, new[] {"К", "K"},
            new[] {"Л", "L"}, new[] {"М", "M"}, new[] {"Н", "N"}, new[] {"О", "O"},
            new[] {"П", "P"}, new[] {"Р", "R"}, new[] {"С", "S"}, new[] {"Т", "T"},
            new[] {"У", "U"}, new[] {"Ф", "F"}, new[] {"Х", "KH"}, new[] {"Ц", "TS"},
            new[] {"Ч", "CH"}, new[] {"Ш", "SH"}, new[] {"Щ", "SCH"}, new[] {"Ъ", ""},
            new[] {"Ы", "Y"}, new[] {"Ь", ""}, new[] {"Э", "E"}, new[] {"Ю", "YU"},
            new[] {"Я", "YA"}, new[] {"а", "a"}, new[] {"б", "b"}, new[] {"в", "v"},
            new[] {"г", "g"}, new[] {"д", "d"}, new[] {"е", "e"}, new[] {"ё", "yo"},
            new[] {"ж", "zh"}, new[] {"з", "z"}, new[] {"и", "i"}, new[] {"й", "y"},
            new[] {"к", "k"}, new[] {"л", "l"}, new[] {"м", "m"}, new[] {"н", "n"},
            new[] {"о", "o"}, new[] {"п", "p"}, new[] {"р", "r"}, new[] {"с", "s"},
            new[] {"т", "t"}, new[] {"у", "u"}, new[] {"ф", "f"}, new[] {"х", "kh"},
            new[] {"ц", "ts"}, new[] {"ч", "ch"}, new[] {"ш", "sh"}, new[] {"щ", "sch"},
            new[] {"ъ", ""}, new[] {"ы", "y"}, new[] {"ь", ""}, new[] {"э", "e"},
            new[] {"ю", "yu"}, new[] {"я", "ya"}
        };

            var result = new StringBuilder(text);

            foreach (var mapping in translitMapping)
            {
                result.Replace(mapping[0], mapping[1]);
            }

            return result.ToString();
        }
    }
}
