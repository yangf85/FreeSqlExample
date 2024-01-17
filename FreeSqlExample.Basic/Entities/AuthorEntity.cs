using FreeSql;
using FreeSql.DataAnnotations;

namespace FreeSqlExample.Basic.Entities
{
    public class AuthorEntity : BaseEntity<AuthorEntity, int>
    {
        [Navigate(nameof(BookEntity.AuthorId))]
        public List<BookEntity> Books { get; set; }

        [Column(Position = 2)]
        public string Name { get; set; }
    }
}