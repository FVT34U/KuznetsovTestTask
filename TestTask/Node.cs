using System.Collections.ObjectModel;

namespace TestTask;

public class Node
{
    private string _name = "";
    private int _value = 0;
    
    public string Name
    {
        get => this._name;
        set
        {
            if (value.Contains("Двигатель"))
            {
                _name = value;
                return;
            }
            _name = $"{value} ({Value}шт)";
        } 
    }

    public int Value
    {
        get => this._value;
        set => this._value = value;
    }
    public ObservableCollection<Node>? Nodes { get; set; }
}