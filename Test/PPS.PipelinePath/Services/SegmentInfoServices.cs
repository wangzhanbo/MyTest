using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace PPS.PipelinePath
{
    public class SegmentInfoServices:ISegmentInfoServices
    {
        private readonly IDBConnectionFactory _connectionFactory;
        public SegmentInfoServices(IDBConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<SegmentInfo> GetSegmentInfo(List<Segment> segments)
        {
            var list = segments.Select(o => o.KeyId);
           return  _connectionFactory.GetInstance().Query<SegmentInfo>(@"
            SELECT  [Name]
                ,a.[KeyId]
                ,a.[KeyValue]
				,b.Consumption
				,b.Length
				,b.MinValue
				,b.MaxValue
				,b.Price
            FROM [PipelineOptimizer_NodeRelation] a
			JOIN [dbo].[PipelineOptimizer_SegmentInfo] b on a.KeyId = b.RelationId
			WHERE a.KeyId in @KeyId", new {  KeyId = list }).ToList();
        }
    }

    public interface ISegmentInfoServices
    {
        List<SegmentInfo> GetSegmentInfo(List<Segment> segments);
    }
}
