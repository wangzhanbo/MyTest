using System;
using Xunit;
using PPS.PipelinePath;
using System.Collections.Generic;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            INodeRelationServices nodeRelationServices =new  TestData();
            PPS.PipelinePath.Controllers.PathsController pathsController = new PPS.PipelinePath.Controllers.PathsController(nodeRelationServices);

            var result=(List<Path>) pathsController.Get("00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000006").Value;
             
            Assert.Equal(2, result.Count);
            Assert.Equal(4, result[0].Segments.Count);
            Assert.Equal(4, result[1].Segments.Count);
        }
         

    }

    public class TestData : INodeRelationServices
    {
        public List<Segment> GetSegments()
        {
            return new List<Segment>()
            {
                new Segment()
                {
                    StartNode = Guid.Parse("00000000-0000-0000-0000-000000000001")
                    ,
                    EndNode = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    ,
                    KeyValue ="1-2"
                },
                new Segment()
                {
                    StartNode = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    ,
                    EndNode = Guid.Parse("00000000-0000-0000-0000-000000000003")
                    ,
                    KeyValue = "2-3"
                },
                new Segment()
                {
                    StartNode = Guid.Parse("00000000-0000-0000-0000-000000000003")
                    ,
                    EndNode = Guid.Parse("00000000-0000-0000-0000-000000000004")
                    ,
                    KeyValue = "3-4"
                },
                new Segment()
                {
                    StartNode = Guid.Parse("00000000-0000-0000-0000-000000000004")
                    ,
                    EndNode = Guid.Parse("00000000-0000-0000-0000-000000000006")
                    ,
                    KeyValue = "4-6"
                },
                new Segment()
                {
                    StartNode = Guid.Parse("00000000-0000-0000-0000-000000000003")
                    ,
                    EndNode = Guid.Parse("00000000-0000-0000-0000-000000000005")
                    ,
                    KeyValue = "3-5"
                },
                new Segment()
                {
                    StartNode = Guid.Parse("00000000-0000-0000-0000-000000000005")
                    ,
                    EndNode = Guid.Parse("00000000-0000-0000-0000-000000000006")
                    ,
                    KeyValue = "5-6"
                },
                new Segment()
                {
                    StartNode = Guid.Parse("00000000-0000-0000-0000-000000000006")
                    ,
                    EndNode = Guid.Parse("00000000-0000-0000-0000-000000000007")
                    ,
                    KeyValue = "6-7"
                },
                new Segment()
                {
                    StartNode = Guid.Parse("00000000-0000-0000-0000-000000000005")
                    ,
                    EndNode = Guid.Parse("00000000-0000-0000-0000-000000000002")
                    ,
                    KeyValue = "5-2"
                }

            };
        }
    }
}
