namespace TestTask;

public class EngineComponent
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Value { get; set; }
    public int? ParentId { get; set; }

    public int NestingLevel { get; set; }

    public Node ToNode()
    {
        return new Node
        {
            ID = Id,
            Value = Value,
            Name = Name,
            ParentId = ParentId,
            NestingLevel = NestingLevel
        };
    }
}