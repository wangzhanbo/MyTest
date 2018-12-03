using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPS.PipelinePath
{
    public class SegmentInfo:Segment
    {
        public float Consumption { get; set; }
        public float Length { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        public float Price { get; set; }
    }
}
