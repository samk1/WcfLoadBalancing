namespace WcfLoadBalancingTestApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Threading;
    using System.Threading.Tasks;

    using WcfLoadBalancing;
    using WcfLoadBalancing.Reference;

    public class Program
    {
        public static void Main(string[] args)
        {
            var endpointAddresses = new List<EndpointAddress>
                                        {
                                            new EndpointAddress("http://localhost:88/Endpoint1/TestService.svc"),
                                            new EndpointAddress("http://localhost:88/Endpoint2/TestService.svc")
                                        };
            var channelFactory =
                new LoadBalancingChannelFactory<ITestServiceChannel>(new BasicHttpBinding(), endpointAddresses);

            var source = new CancellationTokenSource((int)TimeSpan.FromMinutes(5).TotalMilliseconds);
            var tasks = Enumerable.Range(0, 4).Select(i => Task.Run(() => DoWork(channelFactory), source.Token)).ToList();

            Task.WaitAll(tasks.ToArray());
        }

        private static void DoWork(LoadBalancingChannelFactory<ITestServiceChannel> channelFactory)
        {
            var random = new Random();
            while (true)
            {
                Thread.Sleep(1);
                using (var channel = channelFactory.CreateChannel())
                {
                    channel.GetData(random.Next());
                }
            }
        }
    }
}
