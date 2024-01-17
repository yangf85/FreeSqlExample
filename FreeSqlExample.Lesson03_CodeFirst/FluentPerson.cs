using FreeSqlExample.Basic.Entities;

namespace FreeSqlExample.Lesson03_CodeFirst
{
    public class FluentPerson
    {
        public int Age { get; set; } = Random.Shared.Next(1, 36);

        public Gender Gender { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}