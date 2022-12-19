namespace Texas.XunitTests;

public class EmailAddressTests
{
    IConfiguration Configuration { get; set; }

    public EmailAddressTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<PersonTests>();

        Configuration = builder.Build();
    }

    [Fact]
    public void EmailAddressNull_ShouldThrowNullReferenceException()
    {
        // Arrange, Act, Assert
        Action action = () => new EmailAddress(null!);
        action.Should().Throw<NullReferenceException>();
    }
}