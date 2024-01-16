// See https://aka.ms/new-console-template for more information
using FreeSqlExample.Basic;
using FreeSqlExample.Lesson01_CRUD;

internal class Program
{
    private static IFreeSql _orm = BasicOrm.Orm;

    private static void CreateDatabase()
    {
        // 定义数据库名称
        string dbName = BasicOrm.DatabaseName;
        // 检查数据库是否存在的 SQL 命令
        string checkDatabaseExistsSql = $"SELECT database_id FROM sys.databases WHERE Name = '{dbName}'";
        // 执行检查数据库是否存在的命令
        var dbId = _orm.Ado.ExecuteScalar(checkDatabaseExistsSql);
        // 如果 dbId 为 null，说明数据库不存在，需要创建
        if (dbId == null)
        {
            // 定义创建数据库的 SQL 命令
            string createDatabaseSql = $"CREATE DATABASE [{dbName}]";
            // 执行创建数据库的命令
            _orm.Ado.ExecuteNonQuery(createDatabaseSql);
        }
    }

    private static void CreatTable()
    {
        // 使用 CodeFirst 功能同步结构（如果表不存在，则创建）
        _orm.CodeFirst.SyncStructure<CRUDPerson>();
    }

    private static void Delete()
    {
        var id = _orm.Delete<CRUDPerson>().Where(p => p.Id == 1).ExecuteAffrows();
    }

    private static void Insert()
    {
        //插入单条数据
        var p = new CRUDPerson() { Name = "测试" };
        var id = _orm.Insert(p).ExecuteAffrows();
    }

    private static void Main(string[] args)
    {
        var orm = BasicOrm.Orm;
        CreateDatabase();

        CreatTable();
        Insert();
        var list = Query();

        // Delete();
    }

    private static List<CRUDPerson> Query()
    {
        return _orm.Select<CRUDPerson>().ToList();
    }
}