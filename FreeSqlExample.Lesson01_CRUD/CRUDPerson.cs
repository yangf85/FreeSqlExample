using FreeSql.DataAnnotations;

namespace FreeSqlExample.Lesson01_CRUD
{
    [Table(Name = nameof(CRUDPerson))]
    public class CRUDPerson
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}