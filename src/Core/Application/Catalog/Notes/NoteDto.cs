using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public class NoteDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string NoteContent { get; set; }
}
