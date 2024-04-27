using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlExample.Basic.Entities
{
    public class DeliveryReport
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Navigate(ManyToMany = typeof(DeliveryReportPart))]
        public virtual ICollection<Part> Parts { get; set; }
    }

    public class Part
    {
        public int Id { get; set; }

        public string PartName { get; set; }

        public int Piece { get; set; }

        [Navigate(ManyToMany = typeof(DeliveryReportPart))]
        public virtual ICollection<DeliveryReport> DeliveryReports { get; set; }
    }

    public class DeliveryReportPart
    {
        public int DeliveryReportId { get; set; }

        public virtual DeliveryReport DeliveryReport { get; set; }

        public int PartId { get; set; }

        public virtual Part Part { get; set; }

        public int ShippedPiece { get; set; }
    }
}