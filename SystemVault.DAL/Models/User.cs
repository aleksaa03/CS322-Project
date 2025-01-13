using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SystemVault.DAL.Common;

namespace SystemVault.DAL.Models;

[Table("User")]
public class User : Base
{
    [Column(name: "Username", TypeName = "varchar")]
    [StringLength(50)]
    public required string Username { get; set; }

    [Column(name: "Password", TypeName = "text")]
    public required string Password { get; set; }

    [Column(name: "RoleId", TypeName = "smallint")]
    public required UserRole RoleId { get; set; }
}