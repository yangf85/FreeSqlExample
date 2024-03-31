using FreeSql;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlExample.Basic.Entities
{
    public class PartEntity
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int OrderId { get; set; }

        [Navigate(nameof(OrderId))]
        public OrderEntity Order { get; set; }
    }
}