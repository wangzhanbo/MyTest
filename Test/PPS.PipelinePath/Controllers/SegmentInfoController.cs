using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PPS.PipelinePath.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegmentInfoController : ControllerBase
    {
        private ISegmentInfoServices _segmentInfoServices;
        public SegmentInfoController(ISegmentInfoServices segmentInfoServices)
        {
            _segmentInfoServices = segmentInfoServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public ActionResult<IEnumerable<SegmentInfo>> Post(List<Segment> segments)
        {
            List<SegmentInfo> segmentInfos = _segmentInfoServices.GetSegmentInfo(segments);
            var list = from info in segmentInfos
                       join seg in segments on info.KeyId equals seg.KeyId
                       orderby seg._index descending
                       select info;

            return list.ToList();
        }

    }
}