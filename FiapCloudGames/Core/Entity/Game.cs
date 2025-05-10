using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity;
public class Game : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }

    public ICollection<User> Users { get; set; }
    public ICollection<Sale> Sales { get; set; }
}
