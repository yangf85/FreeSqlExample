// See https://aka.ms/new-console-template for more information
using FreeSql.Internal.Model;
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        BuildData();
        PagingQuery();

        MultiTableQuery();

        TreeQuery();

        CascadeQuery();

        ClearData();
    }

    #region 数据创建清除

    private static void BuildData()
    {
        // 创建公司数据
        var companies = new List<CompanyEntity>
            {
                new CompanyEntity { Name = "Company A", CodeName = "CA", Address = "Address A" },
                new CompanyEntity { Name = "Company B", CodeName = "CB", Address = "Address B" },
                new CompanyEntity { Name = "Company C", CodeName = "CC", Address = "Address C" },
                new CompanyEntity { Name = "Company D", CodeName = "CD", Address = "Address D" },
                new CompanyEntity { Name = "Company E", CodeName = "CE", Address = "Address E" }
            };
        // 将公司数据插入数据库并获取自增的Id
        var insertedCompanies = BasicOrm.Orm.Insert(companies).ExecuteInserted();
        // 创建用户数据
        var users = new List<UserEntity>();
        for (int i = 0; i < 100; i++)
        {
            users.Add(new UserEntity
            {
                Username = $"user{i + 1}",
                Email = $"user{i + 1}@example.com",
                CompanyId = insertedCompanies[i % insertedCompanies.Count].Id // 分配公司Id
            });
        }
        // 将用户数据插入数据库
        BasicOrm.Orm.Insert(users).ExecuteAffrows();

        //创建树形数据

        // 省级
        var t1 = BasicOrm.Orm.Insert(new CityEntity { Name = "广东省", ParentID = null, Population = 113460000 }).ExecuteIdentity();
        var t2 = BasicOrm.Orm.Insert(new CityEntity { Name = "四川省", ParentID = null, Population = 83410000 }).ExecuteIdentity();

        // 市级
        var s1 = BasicOrm.Orm.Insert(new CityEntity { Name = "广州市", ParentID = (int)t1, Population = 13500000 }).ExecuteIdentity();
        var s2 = BasicOrm.Orm.Insert(new CityEntity { Name = "深圳市", ParentID = (int)t1, Population = 12500000 }).ExecuteIdentity();
        var s3 = BasicOrm.Orm.Insert(new CityEntity { Name = "成都市", ParentID = (int)t2, Population = 16300000 }).ExecuteIdentity();

        // 区级
        var q1 = BasicOrm.Orm.Insert(new CityEntity { Name = "天河区", ParentID = (int)s1, Population = 1500000 }).ExecuteIdentity();
        var q2 = BasicOrm.Orm.Insert(new CityEntity { Name = "武侯区", ParentID = (int)s3, Population = 1200000 }).ExecuteIdentity();
    }

    private static void ClearData()
    {
        // 删除所有用户数据
        BasicOrm.Orm.Delete<UserEntity>().Where("1=1").ExecuteAffrows();
        // 删除所有公司数据
        BasicOrm.Orm.Delete<CompanyEntity>().Where("1=1").ExecuteAffrows();
        BasicOrm.Orm.Delete<CityEntity>().Where("1=1").ExecuteAffrows();
    }

    #endregion 数据创建清除

    #region 查询

    /// <summary>
    /// 级联查询
    /// </summary>
    private static void CascadeQuery()
    {
        var list1 = BasicOrm.Orm.Select<CompanyEntity>().ToList();

        var list2 = BasicOrm.Orm.Select<CompanyEntity>().IncludeMany(c => c.Users).ToList();

        var user1 = BasicOrm.Orm.Select<UserEntity>().ToOne();
        var user2 = BasicOrm.Orm.Select<UserEntity>().Include(u => u.Company).ToOne();
    }

    /// <summary>
    /// 多表联合查询
    /// </summary>
    private static void MultiTableQuery()
    {
        var list = BasicOrm.Orm.Select<UserEntity>()
                               .InnerJoin<CompanyEntity>((u, c) => u.CompanyId == c.Id && c.Name == "Company B")
                               .ToList();
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    private static void PagingQuery()
    {
        //普通查询
        var total1 = 0l;
        var count1 = 20;
        var list1 = BasicOrm.Orm.Select<UserEntity>()
                              .Count(out total1)
                              .Page((int)total1 / count1, count1).ToList();

        //使用分页类查询
        var page = new BasePagingInfo() { PageNumber = 1, PageSize = 20 };
        var list2 = BasicOrm.Orm.Select<UserEntity>()
                                .Page(page)
                                .ToList();
        var total2 = page.Count;//获取总数
    }

    /// <summary>
    ///  树状查询
    /// </summary>
    private static void TreeQuery()
    {
        var tree = BasicOrm.Orm.Select<CityEntity>().ToTreeList();
    }

    #endregion 查询
}