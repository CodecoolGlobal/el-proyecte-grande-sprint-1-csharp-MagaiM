using System.ComponentModel.DataAnnotations.Schema;
using ElProyecteGrandeSprint1.Models.Enums;

namespace ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string UserName { get; set; }

        public String Password { get; set; }

        public Rank Rank { get; set; }

        public long Reputation { get; set; }

    }
}
