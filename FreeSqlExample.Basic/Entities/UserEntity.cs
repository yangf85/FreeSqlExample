using FreeSql.DataAnnotations;

namespace FreeSqlExample.Basic.Entities
{
    [Table(Name = nameof(UserEntity))]
    public class UserEntity
    {
        public CompanyEntity Company { get; set; }

        // 外键关联 CompanyEntity
        // 导航属性
        [Column(Position = 3)]
        public int CompanyId { get; set; }

        [Column(Position = 2)]
        public string Email { get; set; }

        [Column(IsPrimary = true, IsIdentity = true, Position = 1)]
        public int Id { get; set; }

        [Column(Position = 4)]
        public string Username { get; set; }
    }
}