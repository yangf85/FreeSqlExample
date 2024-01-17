using FreeSql.DataAnnotations;

namespace FreeSqlExample.Basic.Entities
{
    [Table(Name = nameof(UserInfoEntity))]
    public class UserInfoEntity
    {
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public UserEntity User { get; set; }
        public int UserId { get; set; } // 外键关联 UserEntity 导航属性
    }
}