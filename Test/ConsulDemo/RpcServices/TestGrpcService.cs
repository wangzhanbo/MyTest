using MagicOnion;
using MagicOnion.Server;

namespace ConsulDemo.RpcServices
{
    public interface ITestGrpcService : IService<ITestGrpcService>
    {
        UnaryResult<int> Sum(int x, int y);
    }

    public class TestGrpcService : ServiceBase<ITestGrpcService>, ITestGrpcService
    {
        public UnaryResult<int> Sum(int x, int y)
        {
            return UnaryResult(x + y);
        }
    }
}
