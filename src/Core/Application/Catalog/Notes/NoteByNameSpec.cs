using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public class NoteByNameSpec : Specification<Note>, ISingleResultSpecification
{
    public NoteByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}
