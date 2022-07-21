using System.ComponentModel.DataAnnotations.Schema;

namespace ElProyecteGrandeSprint1.Models.Entities.ApiEntities;

public class NewArticle
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Author { get; set; }

    public Theme Theme { get; set; }

    public string? ArticleText { get; set; }
}