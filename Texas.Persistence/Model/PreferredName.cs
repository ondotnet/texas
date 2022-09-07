namespace Texas.Persistence.Model;

public class PreferredName
{
    private string _value;
    public PreferredName(string input)
    {
        _value = input;
    }
    public static implicit operator PreferredName(string input)
    {
        return new(input);
    }
    public override string ToString()
    {
        return _value;
    }
    public override bool Equals(object? obj)
    {
        if (obj is not PreferredName other) { return false; }
        return _value == other._value;
    }
    public override int GetHashCode() => (_value).GetHashCode();

}