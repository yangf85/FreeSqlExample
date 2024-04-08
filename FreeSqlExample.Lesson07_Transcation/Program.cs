// See https://aka.ms/new-console-template for more information

using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

internal class Program
{
    #region 事务

    private static void DbContextTranscation()
    {
        var person1 = new PersonEntity();

        person1.Name = "李秋水";

        var rep = BasicOrm.Orm.GetRepository<PersonEntity>();

        rep.Insert(person1);//插入成功
        using (var ctx = BasicOrm.Orm.CreateDbContext()) //使用的时候会独占数据库
        {
            var pctx = ctx.Set<PersonEntity>();
            var person2 = new PersonEntity();
            person2.Name = "天山童姥";
            pctx.Add(person2);  //新增成功

            var person3 = new PersonEntity();
            person3.Name = "虚竹";
            try
            {
                pctx.Add(person3);
                if (person3.Id > 0)
                {
                    throw new InvalidOperationException();//模拟错误
                }
            }
            catch (Exception)
            {
                throw;
            }

            ctx.SaveChanges();//没有完成提交，那么person2的新增也不会成功
        }
    }

    private static void ExternalTransscation()
    {
        ///TODO
    }

    /// <summary>
    /// 同线程事务，由 fsql.Transaction 管理事务提交回滚（缺点：不支持异步），
    /// 比较适合 WinForm/WPF UI 主线程使用事务的场景。
    /// 同线程事务使用简单，需要注意的限制：
    /// 事务对象在线程挂载，每个线程只可开启一个事务连接，嵌套使用的是同一个事务；
    /// 事务体内代码不可以切换线程，因此不可使用任何异步方法，
    /// 包括 FreeSql 提供的数据库异步方法（可以使用任何 Curd 同步方法）；
    /// </summary>
    private static void SameThreadTransaction()
    {
        var person1 = new PersonEntity();
        person1.Name = "周伯通";
        BasicOrm.Orm.Insert(person1).ExecuteAffrows(); //新增成功

        BasicOrm.Orm.Transaction(() =>
        {
            var person2 = new PersonEntity();
            person2.Name = "镇南王";
            person2 = BasicOrm.Orm.Insert(person2).ExecuteInserted().First();
            person2.Name = "一灯";

            var count = BasicOrm.Orm.Update<PersonEntity>().SetSource(person2).ExecuteAffrows();
            if (count == 1)
            {
                throw new InvalidOperationException();//模拟错误
            }
        });
    }

    private static void UnitOfWorkTranscation()
    {
        var person1 = new PersonEntity();

        person1.Name = "袁承志";

        var rep = BasicOrm.Orm.GetRepository<PersonEntity>();

        rep.Insert(person1);//插入成功

        using (var uw = BasicOrm.Orm.CreateUnitOfWork())
        {
            rep.UnitOfWork = uw;//绑定工作单元 如果不绑定直接使用，没效果

            var person2 = new PersonEntity();
            person2.Name = "温青青";
            rep.Insert(person2);  //新增成功

            var person3 = new PersonEntity();
            person3.Name = "金蛇郎君";
            try
            {
                var pp = rep.Insert(person3);
                if (pp.Id > 1)
                {
                    throw new InvalidOperationException();//模拟错误
                }
            }
            catch (Exception)
            {
                throw;
            }

            uw.Commit();//没有完成提交，那么person2的新增也不会成功
        }
    }

    #endregion 事务

    private static void Main(string[] args)
    {
        //事务依赖仓储模式(Repository)和工作单元(UnitOfWork)
        BasicOrm.ClearTableData<PersonEntity>();
        //UnitOfWorkTranscation();
        //DbContextTranscation();
        SameThreadTransaction();
    }
}