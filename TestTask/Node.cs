using System.Collections.ObjectModel;

namespace TestTask;

public class Node
{
    private string? _name = "";
    private string? _trueName = "";
    private int? _value = 0;
    private int? _parentId = null;
    private int _nestingLevel = 0;
    private int _id = 0;
    
    public string? Name
    {
        get => _name;
        set
        {
            if (value != null && value.Contains("Двигатель"))
            {
                _name = $"{value} {Value}";
                _trueName = _name;
                return;
            }
            _name = $"{value} ({Value}шт)";
            _trueName = value;
        } 
    }

    public int? Value
    {
        get => _value;
        set => this._value = value;
    }

    public int? ParentId
    {
        get => _parentId;
        set => _parentId = value;
    }

    public int NestingLevel
    {
        get => _nestingLevel;
        set => _nestingLevel = value;
    }

    public int ID
    {
        get => _id;
        set => _id = value;
    }

    public ObservableCollection<Node> Nodes { get; set; }

    public Node()
    {
        Nodes = new ObservableCollection<Node>();
    }

    public EngineComponent ToEngineComponent()
    {
        return new EngineComponent
        {
            Id = _id,
            Name = _trueName,
            NestingLevel = _nestingLevel,
            ParentId = _parentId,
            Value = _value
        };
    }

    public string? GetTrueName()
    {
        return _trueName;
    }
}