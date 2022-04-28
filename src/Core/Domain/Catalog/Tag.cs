using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Domain.Catalog;

public class Tag : BaseEntity, IAggregateRoot
{

    public string Name { get; private set; }
    public string? Color { get; private set; }
    public IList<Asset?> Assets { get; private set; }
    public Tag()
    {

    }
    public Tag(string name, string? color, IList<Asset?> assets)
    {
        Name = name;
        Color = color;
        Assets = assets;
    }

  

    public Tag Update(string? name, string? color, IList<Asset?> assets)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (color is not null && Color?.Equals(color) is not true) Color = color;
        if (assets is not null && Assets?.Equals(assets) is not true) Assets = assets;

        return this;

    }
}

