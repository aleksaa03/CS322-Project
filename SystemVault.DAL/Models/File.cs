using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemVault.DAL.Models;

[Table("File")]
public class File : Base
{
    [Column(name: "Name", TypeName = "varchar")]
    [StringLength(100)]
    public required string Name { get; set; }

    [Column(name: "Path", TypeName = "text")]
    public required string Path { get; set; }

    [Column(name: "Size", TypeName = "bigint")]
    public required long Size { get; set; }

    [Column(name: "CreatedAt", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column(name: "CategoryId", TypeName = "bigint")]
    public long CategoryId { get; set; }

    public required virtual Category Category { get; set; }
}