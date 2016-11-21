using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DinJonYa.Plugs.Strings
{
    public class HtmlReg
    {
        public static string StringRegexHtml(string str)
        {
            string regexhtml = @"<[^>]*>";    //去除所有的标签
            string regexscript = @"<script[^>]*?>.*?</script>"; //去除所有脚本，中间部分也删除
            str = Regex.Replace(str, regexscript, string.Empty, RegexOptions.IgnoreCase);
            str = Regex.Replace(str, regexhtml, string.Empty, RegexOptions.IgnoreCase);
            str = str.Replace("&ldquo;", "“");
            str = str.Replace("&rdquo;", "”");
            return str;
        }

        public static string StringRegexHtml(string str, int length)
        {
            string s = StringRegexHtml(str);
            return s.Substring(0, length);
        }
    }
}
