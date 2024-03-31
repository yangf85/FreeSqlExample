using FreeSql;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlExample.Basic.Entities
{
    public class OrderEntity
    {
        [Column(IsPrimary = true)]
        public int Id { get; set; }

        public string Number { get; set; }

        [Navigate(nameof(Id))]
        public PersonEntity Person { get; set; }

        [Navigate(nameof(Id))]
        public SummaryEntity Summary { get; set; }

        [Navigate(nameof(PartEntity.OrderId))]
        public List<PartEntity> Parts { get; set; }

        public string Notes { get; set; }
    }
}