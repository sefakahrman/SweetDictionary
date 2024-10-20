using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetDictionary.Models.Entities;

public class Category : Entity<int>
{

    public string Name { get; set; }

    public List<Post> Posts { get; set; }
}
