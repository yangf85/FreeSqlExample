// See https://aka.ms/new-console-template for more information
using FreeSql;
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

Console.WriteLine("Hello, World!");

BaseEntity.Initialization(BasicOrm.Orm, null);
FreeSql.Internal.Utils.TypeHandlers.TryAdd(typeof(SubComplexType), new StringToSubComplexType());

var com = new ComplexType()
{
    SubComplexType = new SubComplexType()
    {
        Complexes = new List<ComplexPosition>()
        {
            ComplexPosition.Up,
            ComplexPosition.Down,
            ComplexPosition.Left,
            ComplexPosition.Right,
        },
        Name = "复合类型",
        Count = 5,
    },
    Specification = "1000x2000",
};

BasicOrm.Orm.Insert(com).ExecuteAffrows();

var com2 = BasicOrm.Orm.Select<ComplexType>().OrderByDescending(c => c.Id).First();

var cc = com2;