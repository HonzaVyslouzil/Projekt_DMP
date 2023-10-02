using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Article
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public DateTime PublishDate { get; set; }

    public int RssFeedId { get; set; }
    public RssFeed RssFeed { get; set; }
}