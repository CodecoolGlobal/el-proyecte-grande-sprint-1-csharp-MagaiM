using System.ComponentModel.DataAnnotations.Schema;

namespace ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities
{
    public class UserToken
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Token { get; set; }
        public DateTime Date {get; set; }
    }
}