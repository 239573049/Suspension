namespace Gotrays.Contract.Dtos.Systems;

public class GiteeReleaseDto
{
    public int id { get; set; }
    public string tag_name { get; set; }
    public string target_commitish { get; set; }
    public bool prerelease { get; set; }
    public string name { get; set; }
    public string body { get; set; }
    public Author author { get; set; }
    public string created_at { get; set; }
    public Assets[] assets { get; set; }
}

public class Author
{
    public int id { get; set; }
    public string login { get; set; }
    public string name { get; set; }
    public string avatar_url { get; set; }
    public string url { get; set; }
    public string html_url { get; set; }
    public string remark { get; set; }
    public string followers_url { get; set; }
    public string following_url { get; set; }
    public string gists_url { get; set; }
    public string starred_url { get; set; }
    public string subscriptions_url { get; set; }
    public string organizations_url { get; set; }
    public string repos_url { get; set; }
    public string events_url { get; set; }
    public string received_events_url { get; set; }
    public string type { get; set; }
}

public class Assets
{
    public string browser_download_url { get; set; }
    public string name { get; set; }
}