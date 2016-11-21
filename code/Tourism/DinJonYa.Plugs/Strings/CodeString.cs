using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DinJonYa.Plugs.Strings
{
    public static class CodeString
    {
        /// <summary>
        /// 去除代码中的注释部分
        /// </summary>
        /// <param name="codeString">代码字符串</param>
        /// <returns>无注释的代码</returns>
        public  static string CodeStringReplaceComment(string codeString)
        {
            Regex singleLineComment = new Regex(@"//(.*)", RegexOptions.Compiled);
            Regex multiLineComment = new Regex(@"(?<!/)/\*([^*/]|\*(?!/)|/(?<!\*))*((?=\*/))(\*/)", RegexOptions.Compiled | RegexOptions.Multiline);
            Regex RegularString = new Regex("((?<!\\\\)\"([^\"\\\\]|(\\\\.))*\")", RegexOptions.Compiled | RegexOptions.Multiline);
            Regex AtString = new Regex("@(\"([^\"]*)\")(\"([^\"]*)\")*", RegexOptions.Multiline | RegexOptions.Compiled);
            Match mat = null;
            for (int i = 0; i < codeString.Length; i++)
            {
                if (codeString[i] == '\"')
                {
                    mat = RegularString.Match(codeString, i);
                    if (mat.Success)
                    {
                        i = mat.Index + mat.Length;
                    }
                }
                else if (codeString[i] == '@')
                {
                    mat = AtString.Match(codeString, i);
                    if (mat.Success)
                    {
                        i = mat.Index + mat.Length;
                    }
                }
                else if (codeString[i] == '/')
                {
                    mat = singleLineComment.Match(codeString, i);
                    if (mat.Success)
                    {
                        codeString = codeString.Remove(mat.Index, mat.Length);
                        i--;
                    }
                    else
                    {
                        mat = multiLineComment.Match(codeString, i);
                        if (mat.Success)
                        {
                            codeString = codeString.Remove(mat.Index, mat.Length);
                            i--;
                        }
                    }
                }
            }
            return codeString;
        }

        public static string JsonStringConvert(string s)
        {
            char[] temp = s.ToCharArray();
            int n = temp.Length;
            for (int i = 0; i < n; i++)
            {
                if (temp[i] == ':' && temp[i + 1] == '"')
                {
                    for (int j = i + 2; j < n; j++)
                    {
                        if (temp[j] == '"')
                        {
                            if (temp[j + 1] != ',' && temp[j + 1] != '}')
                            {
                                temp[j] = '”';
                            }
                            else if (temp[j + 1] == ',' || temp[j + 1] == '}')
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return new string(temp);
        }
    }
}
