using SystemVault.BLL.DTOs.Category;

namespace SystemVault.BLL.DTOs.ServiceFile;

public class ServiceFileDto : BaseDto
{
    public required string Name { get; set; }
    public required string Path { get; set; }
    public required long Size { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CategoryId { get; set; }
    public required CategoryDto Category { get; set; }
}