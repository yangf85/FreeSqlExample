// See https://aka.ms/new-console-template for more information

using FreeSql;
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

BaseEntity.Initialization(BasicOrm.Orm, null);
BasicOrm.Orm.CodeFirst.SyncStructure<AuthorEntity>();
BasicOrm.ClearTableData<AuthorEntity>();

BasicOrm.Orm.GlobalFilter.Remove("IsDeleted");//移除过滤器

BasicOrm.Orm.Delete<Fruit>().Where("1=1").ExecuteAffrows();//删除全部
var list = BasicOrm.Orm.Select<Fruit>().ToList();
var count = list.Count;//0个

BasicOrm.Orm.GlobalFilter.Apply<ISoftDelete>("IsDeleted", a => a.IsDeleted == false); //应用全局过滤器

var fruit1 = new Fruit() { Name = "苹果" };
var fruit2 = new Fruit() { Name = "梨子" };
var fruit3 = new Fruit() { Name = "香蕉" };
var fruit4 = new Fruit() { Name = "西瓜" };
BasicOrm.Orm.Insert(fruit1).ExecuteAffrows();
BasicOrm.Orm.Insert(fruit2).ExecuteAffrows();
BasicOrm.Orm.Insert(fruit3).ExecuteAffrows();
BasicOrm.Orm.Insert(fruit4).ExecuteAffrows();

//软删除
list = BasicOrm.Orm.Select<Fruit>().ToList();
count = list.Count;//4个
BasicOrm.Orm.Update<Fruit>(list[0]).Set(f => f.IsDeleted, true).ExecuteAffrows(); //软删除

list = BasicOrm.Orm.Select<Fruit>().ToList();
count = list.Count;//3个

BasicOrm.Orm.GlobalFilter.Remove("IsDeleted");//移除过滤器
list = BasicOrm.Orm.Select<Fruit>().ToList();
count = list.Count;//4个