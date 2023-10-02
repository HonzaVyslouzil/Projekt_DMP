using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class RssFeed
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public List<Article> Articles { get; set; }
}