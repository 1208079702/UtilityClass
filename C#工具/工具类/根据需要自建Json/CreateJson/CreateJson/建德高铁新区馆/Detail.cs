using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateJson.建德高铁新区馆
{
    class Detail
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Detail()
        {
            
        }

        public Detail(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
