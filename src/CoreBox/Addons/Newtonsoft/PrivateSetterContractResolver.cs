using System.Reflection;
using CoreBox.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoreBox.Addons.Newtonsoft;

public class PrivateSetterContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        => base.CreateProperty(member, memberSerialization).MakeWriteable(member);
}