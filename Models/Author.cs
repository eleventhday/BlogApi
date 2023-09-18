using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlogApi.Models;

public partial class Author
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

	[JsonIgnore]

	public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
