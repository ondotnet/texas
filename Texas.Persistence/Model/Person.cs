namespace Texas.Persistence.Model;

public class Person : BaseModel
{
    public Title Title { get; set; } = "";
    public LegalName LegalName { get; set; } = "";
    public PreferredName PreferredName { get; set; } = "";
    public Alias Alias { get; set; } = "";
    public EmailAddress PrimaryEmailAddress { get; set; } = "";
}