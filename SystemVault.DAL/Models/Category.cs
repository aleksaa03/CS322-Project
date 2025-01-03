using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemVault.DAL.Models;

[Table("Category")]
public class Category : Base
{
    [Column(name: "Name", TypeName = "varchar")]
    [StringLength(50)]
    public required string Name { get; set; }

    public virtual ICollection<File> Files { get; set; } = [];
}