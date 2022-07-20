using System.ComponentModel.DataAnnotations.Schema;

namespace ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;

public class Article
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public User Author { get; set; }

    public Theme Theme { get; set; }

    public string ArticleText { get; set; }
}