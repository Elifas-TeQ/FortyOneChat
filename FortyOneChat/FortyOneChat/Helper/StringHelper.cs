using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyOneChat.Helper
{
    public static class StringHelper
    {
        public static String TrimEmptyTape(this String value)
        {
            while (!String.IsNullOrEmpty(value) && value[0] == '\n')
            {
                value = value.Remove(0, 1);
            }
            while (!String.IsNullOrEmpty(value) && value[value.Length - 1] == '\n')
            {
                value = value.Remove(value.Length - 1);
            }
            return value;
        }
    }
}
