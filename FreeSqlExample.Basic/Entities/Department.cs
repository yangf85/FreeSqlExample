using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlExample.Basic.Entities
{
    public class Department : BasicEntity
    {
        public string Name { get; set; }

        [Navigate(nameof(PersonEntity.DepartmentId))]
        public List<PersonEntity> Persons { get; set; }
    }
}