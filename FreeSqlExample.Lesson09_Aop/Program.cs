using FreeSql;
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

namespace FreeSqlExample.Lesson09_Aop
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BaseEntity.Initialization(BasicOrm.Orm, null);//初始化需要放在同步表结构之前操作，否则报错id不能为null
            BasicOrm.Orm.Aop.AuditValue += async (s, e) =>
            {
                if (e.AuditValueType == FreeSql.Aop.AuditValueType.Insert && e.Property.Name == nameof(BookEntity.Title))
                {
                    e.Value = await Ret();
                }
            };

            var book = new BookEntity();
            book.Title = "Test";

            var b2 = BasicOrm.Orm.GetRepository<BookEntity>().Insert(book);

            var tt = "1234";
        }

        private static Task<string> Ret()
        {
            return Task.FromResult("66666666");
        }
    }
}