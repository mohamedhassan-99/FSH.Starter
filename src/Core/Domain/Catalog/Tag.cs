using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Domain.Catalog;

public class Tag : BaseEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string? Color { get; set; }
    public ICollection<Asset> Assets { get; set; }

}
