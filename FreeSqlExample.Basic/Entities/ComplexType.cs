using FreeSql;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlExample.Basic.Entities
{
    public enum ComplexPosition
    {
        Up,

        Down,

        Left,

        Right
    }

    public class SubComplexType
    {
        public List<ComplexPosition> Complexes { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }
    }

    [Table]
    public class ComplexType : BaseEntity<ComplexType, int>
    {
        public string Specification { get; set; }

        [Column(MapType = typeof(string), StringLength = -1)]
        public SubComplexType SubComplexType { get; set; }
    }
}