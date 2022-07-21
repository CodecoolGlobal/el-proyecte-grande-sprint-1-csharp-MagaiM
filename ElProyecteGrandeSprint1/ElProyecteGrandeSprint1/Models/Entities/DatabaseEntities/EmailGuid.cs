using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Models;

public class EmailGuid
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    [IgnoreDataMember]
    public long Id { get; set; }
    public Guid Guid { get; set; }
    public string Email { get; set; }
}