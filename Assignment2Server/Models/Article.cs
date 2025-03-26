using System;
using System.Collections.Generic;

namespace Assignment2Server.Models;

public partial class Article
{
    public int ArticleId { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string ContributorUsername { get; set; } = null!;

    public string? ContributorId { get; set; }

    public virtual AspNetUser? Contributor { get; set; }
}
