namespace WcfLoadBalancing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    /// <summary>
    /// Channel factory which applies round robin load balancing.
    /// </summary>
    /// <typeparam name="TChannel">
    /// The type of channel.
    /// </typeparam>
    public class LoadBalancingChannelFactory<TChannel> : ChannelFactoryBase<TChannel>
    {
        private readonly List<ChannelFactory<TChannel>> innerChannelFactories;

        private readonly EventHandler innerFactoryFaultHandler = OnInnerFactoryFault;

        private int channelSeq;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadBalancingChannelFactory{TChannel}"/> class. 
        /// </summary>
        /// <param name="binding">
        /// Binding of the channel factory.
        /// </param>
        /// <param name="endpointAddresses">
        /// List of endpoint addresses to load balance across.
        /// </param>
        public LoadBalancingChannelFactory(Binding binding, List<EndpointAddress> endpointAddresses)
        {
            this.innerChannelFactories = endpointAddresses
                .Select(addr => new ChannelFactory<TChannel>(binding, addr))
                .ToList();

            this.innerChannelFactories.ForEach(
                factory => factory.Faulted += this.innerFactoryFaultHandler);
        }

        public TChannel CreateChannel()
        {
            int channelIndex = this.channelSeq++ % this.innerChannelFactories.Count;
            return this.innerChannelFactories[channelIndex].CreateChannel();
        }

        protected override void OnOpen(TimeSpan timeout)
        {
            this.innerChannelFactories.ForEach(factory => factory.Open(timeout));
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        protected override TChannel OnCreateChannel(EndpointAddress address, Uri via)
        {
            int channelIndex = this.channelSeq++ % this.innerChannelFactories.Count;

            return this.innerChannelFactories[channelIndex].CreateChannel(address, via);
        }

        private static void OnInnerFactoryFault(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
