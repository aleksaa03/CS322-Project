namespace SystemVault.DAL.Models.SearchCriteria;

public abstract class SCBase
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int PageCount { get; set; }
}