// See https://aka.ms/new-console-template for more information
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

internal class Program
{
    private static IFreeSql _orm = BasicOrm.Orm;

    private static void CreatTable()
    {
        // 使用 CodeFirst 功能同步结构（如果表不存在，则创建）
        _orm.CodeFirst.SyncStructure<Person>();
    }

    private static void Delete()
    {
        //条件删除
        var id = _orm.Delete<Person>().Where(p => p.Name == "张无忌").ExecuteAffrows();

        //去重删除(性能低)
        //var list = _orm.Select<Person>().ToList();
        //var indexes = list.DistinctBy(p => p.Name).Select(p => p.Id);
        //_orm.Delete<Person>().Where(p => !indexes.Contains(p.Id)).ExecuteAffrows();

        //去重删除(数据库操作)
        // 使用 Common Table Expression (CTE) 进行去重删除
        string deleteDuplicatesSql = @"
         WITH CTE_Duplicates AS (
             SELECT
                 *,
                 ROW_NUMBER() OVER (PARTITION BY Name ORDER BY Id) AS RowNumber
             FROM
                 Person
         )
         DELETE FROM CTE_Duplicates WHERE RowNumber > 1";
        // 执行删除重复记录的 SQL 命令
        int affectedRows = _orm.Ado.ExecuteNonQuery(deleteDuplicatesSql);
    }

    private static async void Insert()
    {
        //插入单条数据
        var p = new Person() { Name = "张三丰" };
        var count1 = _orm.Insert(p).ExecuteAffrows();

        //插入多条数据
        var list = new List<Person>()
        {
            new Person(){Name="殷天正"},
            new Person(){Name="杨晓"},
            new Person(){Name="灭绝师太",Gender=Gender.Female},
            new Person(){Name="张翠山"},
        };
        var count2 = await _orm.Insert(list).ExecuteAffrowsAsync();
    }

    private static void Main(string[] args)
    {
        CreatTable();
        Insert();
        var list = Query();
        Update();
        Delete();
    }

    private static List<Person> Query()
    {
        return _orm.Select<Person>().Where(p => p.Age > 10 && p.Age < 36).ToList();
    }

    private static void Update()
    {
        //更新单条数据

        var p = _orm.Select<Person>().First();
        if (p != null)
        {
            p.Name = "东方不败";
            var count1 = _orm.Update<Person>().SetSource(p).ExecuteAffrows();
        }

        //更新多条数据

        var count = _orm.Update<Person>()
                           .Set(p => p.Name, "张无忌")
                           .Set(p => p.Age, 14)
                           .Where(p => p.Name == "张三丰").ExecuteAffrows();
    }
}