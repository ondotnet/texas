namespace Texas.XunitTests;

public class PreferredNameTests
{
    IConfiguration Configuration { get; set; }

    public PreferredNameTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<PersonTests>();

        Configuration = builder.Build();
    }

    [Fact]
    public void PreferredNameNull_ShouldSucceed()
    {
        // Arrange, act, assert
        PreferredName actual = new(null!);
        PreferredName actualAnother = new(null!);
        actual.Should().Be(actualAnother);
    }

    [Theory]
    [InlineData("Miss Theron")]
    [InlineData("Barack Obama")]
    [InlineData("Bill Clinton")]
    [InlineData("愛")]
    public void LegalNameNotNull_ShouldSucceed(string legalName)
    {
        // Arrange, act, assert
        PreferredName actual = new(legalName);
        PreferredName actualAnother = new(legalName);
        actual.Should().Be(actualAnother);
    }
}