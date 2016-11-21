using System;
using System.Collections.Generic;
using System.IO;

namespace DinJonYa.Plugs.Strings.SensitiveWordsFilter
{
    /// <summary>
    /// eg. 调用   new WordFilter(SensitiveWords).doFilter("");
    /// </summary>
	public class WordFilter
	{

		private static readonly FilterSet set = new FilterSet();
		private static readonly IDictionary<int?, WordNode> nodes = new Dictionary<int?, WordNode>();

        public WordFilter(IList<string> words)
        {
            addSensitiveWord(words);
        }

		private static void addSensitiveWord(IList<string> words)
		{
			char[] chs;
			int fchar;
			int lastIndex;
			WordNode fnode;
			foreach (string curr in words)
			{
				chs = curr.ToCharArray();
				fchar = chs[0];
				if (!set.contains(fchar))
				{ //没有首字定义
					set.add(fchar); //首字标志位    可重复add,反正判断了，不重复了
					fnode = new WordNode(fchar, chs.Length == 1);
					nodes[fchar] = fnode;
				}
				else
				{
					fnode = nodes[fchar];
					if (!fnode.Last && chs.Length == 1)
					{
						fnode.Last = true;
					}
				}
				lastIndex = chs.Length - 1;
				for (int i = 1; i < chs.Length; i++)
				{
					fnode = fnode.addIfNoExist(chs[i], i == lastIndex);
				}
			}
		}

		private const char SIGN = '*';
		public static string doFilter(string src)
		{
			char[] chs = src.ToCharArray();
			int length = chs.Length;
			int currc;
			int k;
			WordNode node;
			for (int i = 0;i < length;i++)
			{
				currc = chs[i];
				if (!set.contains(currc))
				{
					continue;
				}
	//			k=i;//日	2
				node = nodes[currc]; //日    2
				if (node == null) //其实不会发生，习惯性写上了
				{
					continue;
				}
				bool couldMark = false;
				int markNum = -1;
				if (node.Last)
				{ //单字匹配（日）
					couldMark = true;
					markNum = 0;
				}
				//继续匹配（日你/日你妹），以长的优先
				// 你-3	妹-4		夫-5
				k = i;
				for (; ++k < length;)
				{

					node = node.querySub(chs[k]);
					if (node == null) //没有了
					{
						break;
					}
					if (node.Last)
					{
						couldMark = true;
						markNum = k - i; //3-2
					}
				}
				if (couldMark)
				{
					for (k = 0;k <= markNum;k++)
					{
						chs[k + i] = SIGN;
					}
					i = i + markNum;
				}
			}

			return new string(chs);
		}
	}

}