using FreeSql.DataAnnotations;

namespace FreeSqlExample.Basic.Entities
{
    public enum Gender
    {
        Male,
        Female,
    }

    [Table(Name = nameof(PersonEntity))]
    public class PersonEntity
    {
        [Column(Position = 4)]
        public int Age { get; set; } = Random.Shared.Next(1, 36);

        [Column(Position = 3)]
        public Gender Gender { get; set; }

        [Column(IsPrimary = true, IsIdentity = true, Position = 1)]
        public int Id { get; set; }

        [Column(Position = 2)]
        public string Name { get; set; }
    }
}