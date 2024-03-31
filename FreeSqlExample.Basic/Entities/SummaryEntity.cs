using FreeSql;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlExample.Basic.Entities
{
    public class SummaryEntity : BasicEntity
    {
        [Column(IsPrimary = true)]
        public override int Id { get; set; }

        public string Introduction { get; set; }

        [Navigate(nameof(Id))]
        public OrderEntity Order { get; set; }
    }
}