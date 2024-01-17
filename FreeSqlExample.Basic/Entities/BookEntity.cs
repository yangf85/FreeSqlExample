using FreeSql;
using FreeSql.DataAnnotations;

namespace FreeSqlExample.Basic.Entities
{
    public class BookEntity : BaseEntity<BookEntity, int>
    {
        [Navigate(nameof(AuthorId))]
        public AuthorEntity Author { get; set; }

        [Column(Position = 3)]
        public int AuthorId { get; set; }

        [Column(Position = 2)]
        public string Title { get; set; } // 书籍标题
    }
}