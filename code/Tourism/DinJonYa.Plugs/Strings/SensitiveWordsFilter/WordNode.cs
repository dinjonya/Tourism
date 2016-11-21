using System.Collections.Generic;

namespace DinJonYa.Plugs.Strings.SensitiveWordsFilter
{


	public class WordNode
	{

		private int value;

		private IList<WordNode> subNodes;

		private bool isLast; //默认false

	//	public WordNode(int value){
	//		this.value = value;
	//	}

		public WordNode(int value, bool isLast)
		{
			this.value = value;
	//		if(!this.isLast){//如果已经是某一个敏感词的last，那就是last
			this.isLast = isLast;
	//		}

		}

		/// 
		/// <param name="subNode"> </param>
		/// <returns> 就是传入的subNode </returns>
		private WordNode addSubNode(WordNode subNode)
		{
			if (subNodes == null)
			{
				subNodes = new List<WordNode>();
			}
			subNodes.Add(subNode);
			return subNode;
		}

		/// <summary>
		/// 有就直接返回该子节点， 没有就创建添加并返回该子节点 </summary>
		/// <param name="value">
		/// @return </param>
		public virtual WordNode addIfNoExist(int value, bool isLast)
		{
			if (subNodes == null)
			{
				return addSubNode(new WordNode(value, isLast));
			}
			foreach (WordNode subNode in subNodes)
			{
				if (subNode.value == value)
				{
					if (!subNode.isLast && isLast)
					{
						subNode.isLast = true;
					}
					return subNode;
				}
			}
			return addSubNode(new WordNode(value, isLast));
		}

		public virtual WordNode querySub(int value)
		{
			if (subNodes == null)
			{
				return null;
			}
			foreach (WordNode subNode in subNodes)
			{
				if (subNode.value == value)
				{
					return subNode;
				}
			}
			return null;
		}

		public virtual bool Last
		{
			get
			{
				return isLast;
			}
			set
			{
				this.isLast = value;
			}
		}


		public override int GetHashCode()
		{
			return value;
		}

	}

}