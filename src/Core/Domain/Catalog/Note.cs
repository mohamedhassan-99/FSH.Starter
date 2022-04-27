using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Domain.Catalog;

public class Note : BaseEntity ,IAggregateRoot
{
    public string? NoteContent { get; set; }
    public string Name { get; set; }
    public Guid? AssetId { get; set; }
    public Asset Asset { get; set; }
    public Note(string name, string? noteContent , Guid assetId)
    {
        Name = name;
        NoteContent = noteContent;
        AssetId = assetId;
    }

    public Note Update(string? name, Guid? assetId, string? noteContent)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (noteContent is not null && NoteContent?.Equals(noteContent) is not true) NoteContent = noteContent;
        if (assetId.HasValue && assetId.Value != Guid.Empty && !AssetId.Equals(assetId.Value)) AssetId = assetId.Value;
        return this;
    }
}


