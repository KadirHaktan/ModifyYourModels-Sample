using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static string ReplyToCharToEnglishFormat(this string variable)
        {

            return variable.Replace('ı', 'i')
                .Replace('ü', 'u')
                .Replace('ğ', 'g');
        }

      
    }
}
