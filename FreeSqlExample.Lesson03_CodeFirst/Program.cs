// See https://aka.ms/new-console-template for more information
using FreeSql.DataAnnotations;
using FreeSqlExample.Basic;
using FreeSqlExample.Lesson03_CodeFirst;
using System.Reflection;

var types = GetTypesByTableAttribute();

BasicOrm.Orm.CodeFirst.SyncStructure(types.ToArray());//同步所有的表结构

//外部配置实体属性，开发者不建议使用
BasicOrm.Orm.CodeFirst.ConfigEntity<FluentPerson>(tb =>
{
    tb.Name(nameof(FluentPerson));
    tb.Property(p => p.Id).Name("Id").IsPrimary(true).IsIdentity(true).Position(1);
    tb.Property(p => p.Name).Name("Name").DbType("varchar(48)").Position(2);
    tb.Property(p => p.Gender).Name("Gender").Position(3);
    tb.Property(p => p.Age).Name("Age").Position(4);
});
BasicOrm.Orm.CodeFirst.SyncStructure<FluentPerson>();
BasicOrm.Orm.Insert(new FluentPerson() { Name = "FluentApi" }).ExecuteAffrows();

//获取具有 Table 标签的实体类
static List<Type> GetTypesByTableAttribute()
{
    var asm = Assembly.Load("FreeSqlExample.Basic");
    var types = asm.GetExportedTypes().Where(t => t.GetCustomAttribute<TableAttribute>() != null).ToList();
    return types;
}