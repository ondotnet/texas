namespace Texas.Persistence.Model;

public class LegalName
{
    private string _value;
    public LegalName(string input)
    {
        _value = input;
    }
    public static implicit operator LegalName(string input)
    {
        return new(input);
    }
    public override string ToString()
    {
        return _value;
    }
    public override bool Equals(object? obj)
    {
        if (obj is not LegalName other) { return false; }
        return _value == other._value;
    }
    public override int GetHashCode() => (_value).GetHashCode();

}