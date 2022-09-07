namespace Texas.Persistence.Model;

public class EmailAddress
{
    private static readonly string SENTINEL = "cdj7xc1j@duck.com";
    private static readonly string ERROR_BAD_ADDRESS = "Unable to validate the provided email address. Please check and try again.";
    private string _value;
    public EmailAddress(string input)
    {
        if (string.IsNullOrWhiteSpace(input.Trim()))
        {
            input = SENTINEL;
        }
        System.Net.Mail.MailAddress.TryCreate(input, input, System.Text.Encoding.UTF8, out System.Net.Mail.MailAddress? mailAddress);
        if (mailAddress == null)
        {
            throw new ArgumentException(ERROR_BAD_ADDRESS);
        }
        _value = input;
    }
    public static implicit operator EmailAddress(string input)
    {
        return new EmailAddress(input);
    }
    public override string ToString()
    {
        return _value;
    }
    public override bool Equals(object? obj)
    {
        if (obj is not EmailAddress other) { return false; }
        return _value == other._value;
    }
    public override int GetHashCode() => (_value).GetHashCode();

}
