using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;

namespace ElProyecteGrandeSprint1.Models;

public class ValidatedUser
{
    public long Id { get; set; }
    public string UserName {get; set;}

    public string Email {get; set;}

    public List<string> Roles {get; set;}

    public long Reputation {get; set;}

    public string AccessToken {get; set;}
}