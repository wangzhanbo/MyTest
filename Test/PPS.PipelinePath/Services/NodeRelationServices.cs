using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace PPS.PipelinePath
{

    public class NodeRelationServices : INodeRelationServices
    {
        private readonly IDBConnectionFactory _connectionFactory;
        public NodeRelationServices(IDBConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<Segment> GetSegments()
        {
            return _connectionFactory.GetInstance().Query<Segment>(@"
             SELECT a.[KeyId]
                ,a.[Name]
                ,a.[KeyValue]
                ,a.[StartNode]
                ,a.[EndNode]
				,a.[PipelineId]
				,a.[Sort]
				,b.KeyValue [EndNodeName]
            FROM [PipelineOptimizer_NodeRelation] a
			LEFT JOIN [dbo].[PipelineOptimizer_Node] b on a.EndNode = b.KeyId").ToList();

        }
    }

    public interface INodeRelationServices
    {
        List<Segment> GetSegments();   
    }
}
