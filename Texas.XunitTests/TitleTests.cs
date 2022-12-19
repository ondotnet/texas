namespace Texas.XunitTests;

public class TitleTests
{
    IConfiguration Configuration { get; set; }

    public TitleTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<PersonTests>();

        Configuration = builder.Build();
    }

    [Fact]
    public void PreferredNameNull_ShouldSucceed()
    {
        // Arrange, act, assert
        Title actual = new(null!);
        Title actualAnother = new(null!);
        actual.Should().Be(actualAnother);
    }

    [Theory]
    [InlineData("Miss")]
    [InlineData("Mr.")]
    [InlineData("Esq.")]
    [InlineData("ちゃん")]
    public void LegalNameNotNull_ShouldSucceed(string legalName)
    {
        // Arrange, act, assert
        Title actual = new(legalName);
        Title actualAnother = new(legalName);
        actual.Should().Be(actualAnother);
    }
}