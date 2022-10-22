using System.ComponentModel.DataAnnotations.Schema;

namespace Evico.Entity;

[NotMapped]
public abstract record CategoryRecord : EntityRecord
{
    public String Name { get; set; }
    public String Description { get; set; }
    public CategoryRecord? Parent { get; set; }
}