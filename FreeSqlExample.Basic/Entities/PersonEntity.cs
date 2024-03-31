using FreeSql;
using FreeSql.DataAnnotations;

namespace FreeSqlExample.Basic.Entities
{
    public enum Gender
    {
        Male,

        Female,
    }

    public class PersonEntity
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        [Column(Position = 4)]
        public int Age { get; set; } = Random.Shared.Next(1, 36);

        [Column(Position = 3)]
        public Gender Gender { get; set; }

        [Column(Position = 2)]
        public string Name { get; set; }

        [Navigate(nameof(Id))]
        public OrderEntity Order { get; set; }
    }
}