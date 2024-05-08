using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models;

public class TaskItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("isComplete")]
    public bool IsComplete { get; set; }
}
