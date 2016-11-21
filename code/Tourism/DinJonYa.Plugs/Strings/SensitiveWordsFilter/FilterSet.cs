using System;

namespace DinJonYa.Plugs.Strings.SensitiveWordsFilter
{
	public class FilterSet
	{

		private readonly long[] elements;


		public FilterSet()
		{
			 elements = new long[1 + ((int)((uint)65535 >> 6))];
		}

		public virtual void add(int no)
		{
			elements[(int)((uint)no >> 6)] |= (1L << (no & 63));
		}

		public virtual void add(params int[] no)
		{
			foreach (int currNo in no)
			{
				elements[(int)((uint)currNo >> 6)] |= (1L << (currNo & 63));
			}
		}
		public virtual void remove(int no)
		{
			elements[(int)((uint)no >> 6)] &= ~(1L << (no & 63));
		}

		/// 
		/// <param name="no"> </param>
		/// <returns> true:添加成功	false:原已包含 </returns>
		public virtual bool addAndNotify(int no)
		{
			int eWordNum = (int)((uint)no >> 6);
			long oldElements = elements[eWordNum];
			elements[eWordNum] |= (1L << (no & 63));
			bool result = elements[eWordNum] != oldElements;
			return result;
		}

		/// 
		/// <param name="no"> </param>
		/// <returns> true:移除成功	false:原本就不包含 </returns>
		public virtual bool removeAndNotify(int no)
		{
			int eWordNum = (int)((uint)no >> 6);
			long oldElements = elements[eWordNum];
			elements[eWordNum] &= ~(1L << (no & 63));
			bool result = elements[eWordNum] != oldElements;
	//		if (result)
	//			size--;
			return result;
		}

		public virtual bool contains(int no)
		{
			return (elements[(int)((uint)no >> 6)] & (1L << (no & 63))) != 0;
		}

		public virtual bool containsAll(params int[] no)
		{
			if (no.Length == 0)
			{
				return true;
			}
			foreach (int currNo in no)
			{
				if ((elements[(int)((uint)currNo >> 6)] & (1L << (currNo & 63))) == 0)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 不如直接循环调用contains </summary>
		/// <param name="no">
		/// @return </param>
		public virtual bool containsAll_ueslessWay(params int[] no)
		{
			long[] elements = new long[this.elements.Length];
			foreach (int currNo in no)
			{
				elements[(int)((uint)currNo >> 6)] |= (1L << (currNo & 63));
			} //这一步执行完跟循环调用contains差不多了

			for (int i = 0; i < elements.Length; i++)
			{
				if ((elements[i] & ~this.elements[i]) != 0)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 目前没有去维护size，每次都是去计算size
		/// @return
		/// </summary>
		public virtual int size()
		{
			int size = 0;
			foreach (long element in elements)
			{
				size += bitCount(element);
			}
			return size;
		}
        public int bitCount(long x)
        {
            int count = 0;
            while (x != 0)
            {
                if (x % 2 != 0)
                {  //判断奇偶数  
                    count++;
                }
                x = x >> 1;
            }
            return count;

        }
    }

}