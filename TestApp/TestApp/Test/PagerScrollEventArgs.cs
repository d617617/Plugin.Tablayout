using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Test
{
    public class PagerScrollEventArgs : EventArgs
    {
        /// <summary>
        /// 比例
        /// </summary>
        public double Rate { get; set; }

        public int StartIndex { get; set; }

        /// <summary>
        /// 当前的索引值，在滑动中会变化
        /// </summary>
        public int NowIndex { get; set; }

        /// <summary>
        /// 目标Index
        /// </summary>
        public int TargetIndex { get; set; }

        public int OffsetDirection { get; set; }    

        public int NextPosition { get; set; }
    }
}
