using System;
using System.Collections.Generic;
using System.Text;

namespace Billmanager.Helper
{
    static class StringExtension
    {
        /// <summary>Returns a Value True if empty False if not</summary>
        /// <param name="text">text to check</param>
        /// <returns>True if empty or null False if contains text</returns>
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }
    }
}
