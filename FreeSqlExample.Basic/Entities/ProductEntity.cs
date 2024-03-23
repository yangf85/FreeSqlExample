using FreeSql;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlExample.Basic.Entities
{
    public class ProductEntity : BaseEntity<ProductEntity, int>
    {
        public string Name { get; set; }

        [Column(MapType = typeof(string), StringLength = -1)]
        public List<string> ExampleList1 { get; set; }

        [Column(MapType = typeof(string), StringLength = -1)]
        public List<int> ExampleList2 { get; set; }

        [Column(MapType = typeof(string), StringLength = -1)]
        public List<MaterialSource> Materials { get; set; }
    }
}