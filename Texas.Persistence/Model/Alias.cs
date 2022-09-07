namespace Texas.Persistence.Model;

public class Alias
{
    private string _value;
    public Alias(string alias)
    {
        _value = alias;
    }
    public static implicit operator Alias(string input)
    {
        return new Alias(input);
    }

    public override string ToString()
    {
        return _value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Alias other) { return false; }
        return _value == other._value;
    }

    public override int GetHashCode() => (_value).GetHashCode();
}