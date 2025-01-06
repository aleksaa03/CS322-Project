namespace SystemVault.DAL.Models.SearchCriteria;

public sealed class ServiceFileSC : SCBase
{
    public string? Name { get; set; }
    public long? CategoryId { get; set; }
}