using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Test
{
    public class PagerScrollEvent:EventArgs
    {
        /// <summary>
        /// 比例
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// 目标Index
        /// </summary>
        public int TargetIndex { get; set; }
    }
}
