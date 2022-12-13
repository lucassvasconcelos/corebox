using CoreBox.Addons.Newtonsoft;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace CoreBox.Tests.Addons.Newtonsoft;

public class PrivateSetterContractResolverUnitTests
{
    [Fact]
    public void Deve_Conseguir_Deserializar_Sem_Configuracoes_Adicionais()
    {
        var c = new ClassWithPublicSetProps{ Name = "Test" };

        var cSerialized = JsonConvert.SerializeObject(c);

        var cDeserialized = JsonConvert.DeserializeObject<ClassWithPublicSetProps>(cSerialized);

        cDeserialized.Name.Should().Be("Test");
    }

    [Fact]
    public void Deve_Conseguir_Deserializar_Mesmo_Com_Configuracoes_Adicionais()
    {
        var c = new ClassWithPublicSetProps{ Name = "Test" };

        var cSerialized = JsonConvert.SerializeObject(c);

        var cDeserialized = JsonConvert.DeserializeObject<ClassWithPublicSetProps>(cSerialized, new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() });

        cDeserialized.Name.Should().Be("Test");
    }

    [Fact]
    public void Nao_Deve_Conseguir_Deserializar_Sem_Configuracoes_Adicionais()
    {
        var c = ClassWithPrivateSetProps.Create("Test");

        var cSerialized = JsonConvert.SerializeObject(c);

        var cDeserialized = JsonConvert.DeserializeObject<ClassWithPrivateSetProps>(cSerialized);

        cDeserialized.Name.Should().BeNull();
    }

    [Fact]
    public void Deve_Conseguir_Deserializar_Com_Configuracoes_De_Propriedades_Privadas()
    {
        var c = ClassWithPrivateSetProps.Create("Test");

        var cSerialized = JsonConvert.SerializeObject(c);

        var cDeserialized = JsonConvert.DeserializeObject<ClassWithPrivateSetProps>(cSerialized, new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() });

        cDeserialized.Name.Should().Be("Test");
    }

    [Fact]
    public void Deve_Conseguir_Deserializar_Com_Configuracoes_De_Propriedades_Privadas_E_Lidar_Com_Propriedades_Inexistentes()
    {
        var cDeserialized = JsonConvert.DeserializeObject<ClassWithPrivateSetProps>("{\"Name\": \"Test\", \"Test\": \"Test\"}", new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() });

        cDeserialized.Name.Should().Be("Test");
    }
}

public class ClassWithPublicSetProps { public string Name { get; set; } }
public class ClassWithPrivateSetProps
{
    public string Name { get; private set; }
    public static ClassWithPrivateSetProps Create(string name)
    {
        return new ClassWithPrivateSetProps { Name = name };
    }
}