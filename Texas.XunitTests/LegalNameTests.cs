namespace Texas.XunitTests;

public class LegalNameTests
{
    IConfiguration Configuration { get; set; }

    public LegalNameTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<PersonTests>();

        Configuration = builder.Build();
    }

    /// <summary>
    /// TODO: figure out what makes this different from email address
    /// </summary>
    [Fact]
    public void LegalNameNull_ShouldSucceed()
    {
        // Arrange, act, assert
        LegalName actual = new(null!);
        LegalName actualAnother = new(null!);
        actual.Should().Be(actualAnother);
    }

    [Theory]
    [InlineData("Charlize Theron")]
    [InlineData("Barack Hussein Obama II")]
    [InlineData("William Jefferson Blythe III")]
    [InlineData("篠崎 愛")]
    public void LegalNameNotNull_ShouldSucceed(string legalName)
    {
        // Arrange, act, assert
        LegalName actual = new(legalName);
        LegalName actualAnother = new(legalName);
        actual.Should().Be(actualAnother);
    }
}