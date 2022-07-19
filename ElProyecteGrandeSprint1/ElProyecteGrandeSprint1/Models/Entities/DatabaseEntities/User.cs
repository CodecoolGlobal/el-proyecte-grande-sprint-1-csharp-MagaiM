using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ElProyecteGrandeSprint1.Models.Enums;

namespace ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        [IgnoreDataMember]
        public Password Password { get; set; }

        public HashSet<UserRole> Roles { get; set; }

        public long Reputation { get; set; }

    }
}
