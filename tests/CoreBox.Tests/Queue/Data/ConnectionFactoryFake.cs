using System;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace CoreBox.Tests.Queue.Data
{
    public class ConnectionFactoryFake : IConnectionFactory
    {
        public IDictionary<string, object> ClientProperties { get; set; }
        public string Password { get; set; }
        public ushort RequestedChannelMax { get; set; }
        public uint RequestedFrameMax { get; set; }
        public TimeSpan RequestedHeartbeat { get; set; }
        public bool UseBackgroundThreadsForIO { get; set; }
        public string UserName { get; set; }
        public string VirtualHost { get; set; }
        public Uri Uri { get; set; }
        public string ClientProvidedName { get; set; }
        public TimeSpan HandshakeContinuationTimeout { get; set; }
        public TimeSpan ContinuationTimeout { get; set; }

        public IAuthMechanismFactory AuthMechanismFactory(IList<string> mechanismNames)
        {
            return default!;
        }

        public virtual IConnection CreateConnection() =>
            new ConnectionFake();

        public IConnection CreateConnection(string clientProvidedName)
        {
            return default!;
        }

        public IConnection CreateConnection(IList<string> hostnames)
        {
            return default!;
        }

        public IConnection CreateConnection(IList<string> hostnames, string clientProvidedName)
        {
            return default!;
        }

        public IConnection CreateConnection(IList<AmqpTcpEndpoint> endpoints)
        {
            return default!;
        }

        public IConnection CreateConnection(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName)
        {
            return default!;
        }
    }
}