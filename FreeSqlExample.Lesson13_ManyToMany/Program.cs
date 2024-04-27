// See https://aka.ms/new-console-template for more information
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

Console.WriteLine("Hello, World!");

BasicOrm.ClearTableData<Part>();

var parts = new List<Part>
{
    new Part { PartName = "Part1",Piece=35 },
    new Part { PartName = "Part2",Piece=27 }
};

BasicOrm.Orm.Insert(parts);

// 创建一个新的 DeliveryReport 实例
var deliveryReport = new DeliveryReport
{
    Name = "New Report"
};

BasicOrm.Orm.Insert(deliveryReport);