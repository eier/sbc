using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sbc11WorkflowService
{
    public static class ExtensionMethods
    {
        public static string ToDetailedString<T>(this IEnumerable<T> me, string seperator = ", ", string formatString = "{0}")
        {
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;

            foreach (T item in me)
            {
                if (isFirst)
                    isFirst = false;
                else
                    sb.Append(seperator);

                sb.AppendFormat(formatString, item);
            }

            return sb.ToString();
        }
    }
}
