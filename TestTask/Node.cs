using System.Collections.ObjectModel;

namespace TestTask;

public class Node
{
    private string? _name = "";
    private int? _value = 0;
    private int? _parentId = null;
    
    public string? Name
    {
        get => _name;
        set
        {
            if (value != null && value.Contains("Двигатель"))
            {
                _name = $"{value} {Value}";
                return;
            }
            _name = $"{value} ({Value}шт)";
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
    
    public ObservableCollection<Node>? Nodes { get; set; }
}