using System.Collections;
using System.Collections.Generic;
using CoreBox.Queue.Shared;
using RabbitMQ.Client;

namespace CoreBox.Tests.Queue.Data;

public class InvalidRequest : IEnumerable<object[]>
{
    private List<object[]> Get_data()
    {
        return new List<object[]>
        {
            new object[] { null, null, "As configurações de publicação na fila são obrigatórias" },
            new object[] { new ConnectionFactory(), null, "As configurações de conexão com a fila são obrigatórias" }
        };
    }

    public IEnumerator<object[]> GetEnumerator() => Get_data().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
