namespace Texas.Persistence.Model;

public class Title
{
    private string _value;
    public Title(string input)
    {
        _value = input;
    }
    public static implicit operator Title(string input)
    {
        return new(input);
    }

    public override string ToString()
    {
        return _value;
    }
    public override bool Equals(object? obj)
    {
        if (obj is not Title other) { return false; }
        return _value == other._value;
    }
    public override int GetHashCode() => (_value).GetHashCode();
}