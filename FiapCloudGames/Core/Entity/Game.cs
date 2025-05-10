using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity;
public class Game : EntityBase
{
    public required string Name { get; set; }
    public string Description { get; set; }
    public required string Genre { get; set; }
    public required decimal Price { get; set; }

    public ICollection<Library> Libraries { get; set; }
    public ICollection<Sale> Sales { get; set; }
}
