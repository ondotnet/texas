namespace Texas.XunitTests;

public class PersonTests
{
    IConfiguration Configuration { get; set; }

    public PersonTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<PersonTests>();

        Configuration = builder.Build();
    }
    [Fact]
    public void Person_ShouldHaveANonNullPrimaryEmailAddress()
    {
        // Arrange, Act
        Person person = new();

        // Assert
        person.PrimaryEmailAddress.Should().NotBeNull();
    }

    [Fact]
    public async void Person_ShouldHaveReturn()
    {
        // Arrange
        string? _connectionString = string.IsNullOrWhiteSpace(Configuration["ConnectionStrings:Bitio"]) 
            ? Configuration["ConnectionStrings:Default"] : Configuration["ConnectionStrings:Bitio"];
        if (!string.IsNullOrWhiteSpace(_connectionString))
        {
            Mock<ILogger<PersonDb>> mock = new();
            ILogger<PersonDb> logger = mock.Object;
            PersonDb access = new(_connectionString, logger);
            Person expected = new()
            {
                Title = new Title("Mr"),
                LegalName = new LegalName("Shawn Corey Carter"),
                PreferredName = new PreferredName("Jay Z"),
                Alias = new Alias("v-shawncarter"),
                PrimaryEmailAddress = new EmailAddress("v-shawncarter@whitehouse.gov")
            };

            //Act
            IEnumerable<Person> result = await access.GetPeopleAsync();
            List<Person> resultList = result.ToList<Person>();

            // Assert
            resultList.Should().NotBeNull();
            resultList.Count.Should().Be(4); // we know this because we wrote the script in create database
            resultList.Where(x => x.Id == 4).Count().Should().Be(1);
            resultList.Where(x => x.Id == 4).First().ExternalId.Should().NotBeEmpty();
            resultList.Where(x => x.Id == 4).First().Title.Should().BeEquivalentTo(expected.Title);
            resultList.Where(x => x.Id == 4).First().LegalName.Should().BeEquivalentTo(expected.LegalName);
            resultList.Where(x => x.Id == 4).First().PreferredName.Should().BeEquivalentTo(expected.PreferredName);
            resultList.Where(x => x.Id == 4).First().Alias.Should().BeEquivalentTo(expected.Alias);
            resultList.Where(x => x.Id == 4).First().PrimaryEmailAddress.Should().BeEquivalentTo(expected.PrimaryEmailAddress);
            resultList.Where(x => x.Id == 4).First().CreatedBy.Should().Be(1);
            resultList.Where(x => x.Id == 4).First().CreatedDate.Should().BeAfter(DateTime.MinValue);
            resultList.Where(x => x.Id == 4).First().CreatedDate.Should().BeBefore(DateTime.UtcNow);
            resultList.Where(x => x.Id == 4).First().ModifiedBy.Should().Be(1);
            resultList.Where(x => x.Id == 4).First().ModifiedDate.Should().BeAfter(DateTime.MinValue);
            resultList.Where(x => x.Id == 4).First().ModifiedDate.Should().BeBefore(DateTime.UtcNow); 
        }
    }

    [Fact]
    public async void Person_ShouldUpdate()
    {
        // Arrange
        string? _connectionString = string.IsNullOrWhiteSpace(Configuration["ConnectionStrings:Bitio"])
            ? Configuration["ConnectionStrings:Default"] : Configuration["ConnectionStrings:Bitio"];
        if (!string.IsNullOrWhiteSpace(_connectionString))
        {
            Mock<ILogger<PersonDb>> mock = new();
            ILogger<PersonDb> logger = mock.Object;
            PersonDb access = new(_connectionString, logger);
            Person expected = new()
            {
                Title = new Title("Mr"),
                LegalName = new LegalName("Shawn Corey Carter"),
                PreferredName = new PreferredName("Jay Z"),
                Alias = new Alias("v-shawncarter"),
                PrimaryEmailAddress = new EmailAddress("v-shawncarter@whitehouse.gov")
            };

            //Act
            IEnumerable<Person> result = await access.GetPeopleAsync();
            List<Person> resultList = result.ToList<Person>();

            // Assert
            resultList.Should().NotBeNull();
            resultList.Count.Should().Be(4); // we know this because we wrote the script in create database
            resultList.Where(x => x.Id == 4).Count().Should().Be(1);
            resultList.Where(x => x.Id == 4).First().ExternalId.Should().NotBeEmpty();
            resultList.Where(x => x.Id == 4).First().Title.Should().BeEquivalentTo(expected.Title);
            resultList.Where(x => x.Id == 4).First().LegalName.Should().BeEquivalentTo(expected.LegalName);
            resultList.Where(x => x.Id == 4).First().PreferredName.Should().BeEquivalentTo(expected.PreferredName);
            resultList.Where(x => x.Id == 4).First().Alias.Should().BeEquivalentTo(expected.Alias);
            resultList.Where(x => x.Id == 4).First().PrimaryEmailAddress.Should().BeEquivalentTo(expected.PrimaryEmailAddress);
            resultList.Where(x => x.Id == 4).First().CreatedBy.Should().Be(1);
            resultList.Where(x => x.Id == 4).First().CreatedDate.Should().BeAfter(DateTime.MinValue);
            resultList.Where(x => x.Id == 4).First().CreatedDate.Should().BeBefore(DateTime.UtcNow);
            resultList.Where(x => x.Id == 4).First().ModifiedBy.Should().Be(1);
            resultList.Where(x => x.Id == 4).First().ModifiedDate.Should().BeAfter(DateTime.MinValue);
            resultList.Where(x => x.Id == 4).First().ModifiedDate.Should().BeBefore(DateTime.UtcNow);

            foreach (var person in resultList)
            {
                var actual = await access.UpdatePersonPrimaryEmailAddress(person.Id, person.PrimaryEmailAddress.ToString());
                actual.Should().Be(1);
                IEnumerable<Person> result2 = await access.GetPeopleAsync();
                List<Person> resultList2 = result.ToList<Person>();

                // Assert
                resultList2.Should().NotBeNull();
                resultList2.Count.Should().Be(4); // we know this because we wrote the script in create database
                resultList2.Where(x => x.Id == 4).Count().Should().Be(1);
                resultList2.Where(x => x.Id == 4).First().ExternalId.Should().NotBeEmpty();
                resultList2.Where(x => x.Id == 4).First().Title.Should().BeEquivalentTo(expected.Title);
                resultList2.Where(x => x.Id == 4).First().LegalName.Should().BeEquivalentTo(expected.LegalName);
                resultList2.Where(x => x.Id == 4).First().PreferredName.Should().BeEquivalentTo(expected.PreferredName);
                resultList2.Where(x => x.Id == 4).First().Alias.Should().BeEquivalentTo(expected.Alias);
                resultList2.Where(x => x.Id == 4).First().PrimaryEmailAddress.Should().BeEquivalentTo(expected.PrimaryEmailAddress);
                resultList2.Where(x => x.Id == 4).First().CreatedBy.Should().Be(1);
                resultList2.Where(x => x.Id == 4).First().CreatedDate.Should().BeAfter(DateTime.MinValue);
                resultList2.Where(x => x.Id == 4).First().CreatedDate.Should().BeBefore(DateTime.UtcNow);
                resultList2.Where(x => x.Id == 4).First().ModifiedBy.Should().Be(1);
                resultList2.Where(x => x.Id == 4).First().ModifiedDate.Should().BeAfter(DateTime.MinValue);
                resultList2.Where(x => x.Id == 4).First().ModifiedDate.Should().BeBefore(DateTime.UtcNow);
            }
        }
    }
}