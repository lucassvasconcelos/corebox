using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace CoreBox.Extensions;

public static class JsonPropertyExtensions
{
    public static JsonProperty MakeWriteable(this JsonProperty jProperty, MemberInfo member)
    {
        if (jProperty.Writable)
            return jProperty;

        var property = member as PropertyInfo;
        jProperty.Writable = property!.SetMethod != null;
        return jProperty;
    }
}