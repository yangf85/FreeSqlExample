using FreeSql.DataAnnotations;

namespace FreeSqlExample.Basic.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; }
    }

    public abstract class BasicEntity : ISoftDelete
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public virtual int Id { get; set; }

        [Column(Position = 997, ServerTime = DateTimeKind.Utc, CanUpdate = false)]
        public DateTime CreateTime { get; set; }

        [Column(Position = 998, ServerTime = DateTimeKind.Utc)]
        public DateTime UpdateTime { get; set; }

        [Column(Position = 999)]
        public bool IsDeleted { get; set; }
    }
}