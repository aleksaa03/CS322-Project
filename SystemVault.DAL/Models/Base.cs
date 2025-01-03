using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemVault.DAL.Models;

public abstract class Base
{
    [Key]
    [Column(name: "Id")]
    public long Id { get; set; }
}