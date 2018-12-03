using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PPS.PipelinePath.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PathsController : ControllerBase
    {

        private List<Segment> segments;
        private Stack st;
        private List<Segment> cacheSegments;
        public PathsController(INodeRelationServices relationServices)
        {
            segments = relationServices.GetSegments();
            st = new Stack();
            cacheSegments = new List<Segment>();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Path>> Get(string startNode, string endNode)
        {
            //https://blog.csdn.net/ha000/article/details/52368566

            List<Path> paths = new List<Path>();
            if (startNode.ToLower() == endNode.ToLower())
                return paths;

            Segment segment = new Segment();
            segment.EndNode = Guid.Parse(startNode);
            st.Push(segment);

            while (st.Count > 0)
            {
                //获得栈顶元素
                var v = (Segment)st.Peek();

                //获得列表
                var list = segments.Where(p => p.StartNode.ToString().ToLower() == v.EndNode.ToString().ToLower() && p.IsRead == false).ToList();

                //已经访问过的的，排除出去
                foreach (var item in cacheSegments)
                {
                    list.Remove(item);
                }

                //证明还有子节点
                if (list.Count > 0)
                {
                    if (list[0].EndNode.ToString().ToLower() != endNode.ToLower())
                    {
                        list[0].IsRead = true;
                        st.Push(list[0]);
                        cacheSegments.Add(list[0]);
                    }
                    else
                    {
                        v = list[0];
                        cacheSegments.Add(list[0]);
                    }

                }
                else
                {
                    v = (Segment)st.Pop();
                }

                if (v.EndNode.ToString().ToLower() == endNode.ToLower())
                {
                    Path path = new Path();
                    v.IsRead = false;

                    path.Segments.Add(v);
                    int idx = 1;
                    foreach (var item in st.ToArray())
                    {
                        var seg = (Segment)item;
                        if (seg.KeyValue!=null)
                        {
                            seg._index = idx;
                            path.Segments.Add(seg);
                            idx++;
                        }
                    }

                    path.Segments = path.Segments.OrderByDescending(o => o._index).ToList();
                    paths.Add(path);
                }
            }



            return paths;

        }

        Segment findNode(Segment segment, string startNode, string endNode)
        {
            if (segment.EndNode.ToString().ToLower() == startNode.ToLower())
            {
                //推一个空进去，表示一个路劲
                st.Push(null);
            }

            if (segment.EndNode.ToString().ToLower() == endNode.ToLower())
            {
                return segment;
            }
            else
            {
                //找出所有的下级管段
                var end = segments.Where(p => p.StartNode == segment.EndNode).ToList();

                var list = end.Except(cacheSegments).ToList();

                Segment _seg = null;
                foreach (var seg in list)
                {
                    cacheSegments.Add(seg);
                    _seg = findNode(seg, startNode, endNode);

                    if (_seg != null)
                        st.Push(_seg);
                }

                return segment;
            }
        }


    }
}