// See https://aka.ms/new-console-template for more information
using FreeSql;
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

Console.WriteLine("Hello, World!");

//BaseEntity.Initialization(BasicOrm.Orm, null);//初始化需要放在同步表结构之前操作，否则报错id不能为null

var rep1 = BasicOrm.Orm.GetRepository<PersonEntity>();
var rep2 = BasicOrm.Orm.GetRepository<OrderEntity>();
var rep3 = BasicOrm.Orm.GetRepository<SummaryEntity>();
var rep4 = BasicOrm.Orm.GetRepository<PartEntity>();

rep1.Select.ToDelete().Where("1=1").ExecuteAffrows();
rep2.Select.ToDelete().Where("1=1").ExecuteAffrows();
rep3.Select.ToDelete().Where("1=1").ExecuteAffrows();
rep4.Select.ToDelete().Where("1=1").ExecuteAffrows();

Sample1();
//Sample2();

static void Sample1()
{
    //1对1导航属性 只需要在1个主类上设置Id自增 ,否则抛出异常ID有值
    var person = new PersonEntity()
    {
        Name = "张三",
        Age = 18,
        Gender = Gender.Male,
        Order = new OrderEntity()
        {
            Number = "AABBCC",
            Summary = new SummaryEntity()
            {
                Introduction = "这是一个介绍"
            },
            Parts = new List<PartEntity>()
            {
                new PartEntity(){Name="手套",Price=3.6},
        new PartEntity(){Name="帽子",Price=2.1},
            }
        }
    };

    var rep1 = BasicOrm.Orm.GetRepository<PersonEntity>();

    //自动保存
    rep1.DbContextOptions.EnableCascadeSave = true;
    rep1.Insert(person);

    var p = BasicOrm.Orm.Select<PersonEntity>()
               .Include(p => p.Order)
               .Include(p => p.Order.Summary)
               .IncludeMany(p => p.Order.Parts)
               .ToOne();
}

static void Sample2()
{
    //暂时不知道怎么做
    var rep = BasicOrm.Orm.GetAggregateRootRepository<PersonEntity>();
    var p = rep.Select.ToOne();
}