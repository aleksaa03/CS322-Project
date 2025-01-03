using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SystemVault.DAL.Models;

[Table("User")]
public class User : Base
{
    [Column(name: "Username", TypeName = "varchar")]
    [StringLength(50)]
    public required string Username { get; set; }

    [Column(name: "Password", TypeName = "text")]
    public required string Password { get; set; }
}