using System;
using System.Collections.Generic;

namespace BlogApi.Models;

public partial class Blog
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Content { get; set; }

    public string? ImagePath { get; set; }

    public bool IsPublish { get; set; }

    public int AuthorId { get; set; }

    public int CategoryId { get; set; }

    public DateTime CreateTime { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;
}
