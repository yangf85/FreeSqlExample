using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlExample.Basic.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; }
    }

    public class Fruit : ISoftDelete
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}