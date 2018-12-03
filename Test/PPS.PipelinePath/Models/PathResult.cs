using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPS.PipelinePath
{
    /// <summary>
    /// 节点
    /// </summary>
    public class Node
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }


    /// <summary>
    /// 管段
    /// </summary>
    public class Segment
    {
        public Guid KeyId { get; set; }
        public string Name { get; set; }
        public string KeyValue { get; set; }
        public Guid StartNode { get; set; }

        public Guid EndNode { get; set; }
        public string EndNodeName { get; set; }

        public int _index { get; set; }
        public bool IsRead { get; set; }

    }

    /// <summary>
    /// 返回路径
    /// </summary>
    public class Path
    {
        public Path()
        {
            _segments = new List<Segment>();
        }
        private List<Segment> _segments { get; set; }
        public List<Segment> Segments
        {
            get { return _segments; }
            set { _segments = value; }
        }
    }

}
