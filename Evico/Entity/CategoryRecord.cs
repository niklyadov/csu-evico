using System.ComponentModel.DataAnnotations.Schema;

namespace Evico.Entity;

[NotMapped]
public abstract record CategoryRecord : EntityRecord
{
    public String Name { get; set; } = String.Empty;
    public String Description { get; set; } = String.Empty;
    public CategoryRecord? Parent { get; set; }
}