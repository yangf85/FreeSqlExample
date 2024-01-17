using FreeSql.DataAnnotations;

namespace FreeSqlExample.Basic.Entities
{
    [Table(Name = nameof(CompanyEntity))]
    public class CompanyEntity
    {
        [Column(Position = 4)]
        public string Address { get; set; }

        [Column(Position = 3)]
        public string CodeName { get; set; }

        [Column(Position = 1, IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        [Column(Position = 2, DbType = "varchar(128) NOT NULL")]
        public string Name { get; set; }

        [Navigate(nameof(UserEntity.CompanyId))]
        public List<UserEntity> Users { get; set; } // 导航属性
    }
}