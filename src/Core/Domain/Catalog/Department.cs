using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Domain.Catalog;

public class Department : BaseEntity,IAggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Department( string? description, string name)
    {
        Description = description;
        Name = name;
    }
    public Department Update(string desc, string name)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (desc is not null && Description?.Equals(desc) is not true) Description = desc;
        return this;

    }
}
