// See https://aka.ms/new-console-template for more information
using FreeSqlExample.Basic;

Console.WriteLine("==========获取所有的数据库!");
var databases = BasicOrm.Orm.DbFirst.GetDatabases();//因为数据库连接字符串已经指定了数据库的名称，所以这里只会获取主数据库

foreach (var database in databases)
{
    Console.WriteLine($"{database}");
}

Console.WriteLine("==========返回所有的表、列详情、主键、唯一键、索引、外键、备注等等!");
var tableInfos = BasicOrm.Orm.DbFirst.GetTablesByDatabase(BasicOrm.Orm.DbFirst.GetDatabases().First());
foreach (var tableInfo in tableInfos)
{
    Console.WriteLine($"表主键:{tableInfo.Id}");
    Console.WriteLine($"表名称:{tableInfo.Name}");
    Console.WriteLine($"表备注:{tableInfo.Comment}");
}

Console.WriteLine("==========返回特定表的列详情、主键、唯一键、索引、备注等等");

var personTable = BasicOrm.Orm.DbFirst.GetTableByName("Person");

if (personTable is not null)
{
    Console.WriteLine($"表主键:{personTable.Id}");
    Console.WriteLine($"表名称:{personTable.Name}");
    Console.WriteLine($"表列名:{string.Join("&", personTable.Columns.Select(c => c.Name))}");
    Console.WriteLine($"表类型:{personTable.Type}");
}

Console.WriteLine("==========返回特定列的C# 值");
var columnValue = BasicOrm.Orm.DbFirst.GetCsTypeValue(personTable.Columns.Where(c => c.Name == "Name").First());
Console.WriteLine(columnValue);