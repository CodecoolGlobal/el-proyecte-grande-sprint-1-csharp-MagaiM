using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities
{
    public class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [IgnoreDataMember]
        public long Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [IgnoreDataMember]
        public HashSet<User> Users { get; set; } = new HashSet<User>();
    }
}
