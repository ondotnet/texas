namespace Texas.Persistence.DataAccess;

public class PersonDb: IPersonDb
{
    //private readonly string _connectionString = "Host=hansken.db.elephantsql.com;Database=xrbmpoui;User Id=xrbmpoui;Password=i38x7v1O3aNteoNxteJNB5thtPfKqqxn;";
    private readonly string _connectionString;
    private readonly ILogger<PersonDb> _logger;
    public PersonDb(string connectionString, ILogger<PersonDb> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetPeopleAsync(string orderby = "id", int limit = 200, int offset = 0)
    {
        using NpgsqlConnection connection = new(_connectionString);
        Stopwatch stopwatch = Stopwatch.StartNew();
        IEnumerable<Person> persons = await connection.QueryAsync<Person>(
            @"select
                person.*,
                createdby.alias as createdbyname, 
                modifiedby.alias as modifiedbyname
            from person person
            left join person createdby on createdby.id = person.createdby
            left join person modifiedby on modifiedby.id = person.modifiedby
            order by @orderby
            limit @limit
            offset @offset
            ;", new 
            { 
                orderby = @orderby,
                limit = @limit,
                offset = @offset
            });
        stopwatch.Stop();
        foreach (Person person in persons)
        {
            person.Stopwatch = stopwatch;
        }
        _logger.LogDebug("{methodName} returned {result}", nameof(GetPeopleAsync), JsonConvert.SerializeObject(persons));
        return persons;
    }

    public async Task<int> UpdatePersonPrimaryEmailAddress(Person person)
    {
        using NpgsqlConnection connection = new(_connectionString);
        Stopwatch stopwatch = Stopwatch.StartNew();
        var result = await connection.ExecuteAsync(@"update person p set primaryemailaddress = @email where p.id = @personId", new { personId = person.Id, email = person.PrimaryEmailAddress.ToString() });
        return result;
    }
}
