using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CoreBox.Addons.MongoDriver;

[ExcludeFromCodeCoverage]
public class TimeOnlyBsonSerializer : StructSerializerBase<TimeOnly>
{
    private readonly IBsonSerializer<TimeSpan> _timeSpanSerializer = new TimeSpanSerializer();

    public override TimeOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => TimeOnly.FromTimeSpan(_timeSpanSerializer.Deserialize(context, args));

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TimeOnly value)
        => _timeSpanSerializer.Serialize(context, args, value.ToTimeSpan());
}