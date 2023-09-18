using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlogApi.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
