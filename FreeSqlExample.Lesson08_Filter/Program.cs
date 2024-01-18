// See https://aka.ms/new-console-template for more information

using FreeSql;
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

BaseEntity.Initialization(BasicOrm.Orm, null);
BasicOrm.Orm.CodeFirst.SyncStructure<AuthorEntity>();
BasicOrm.ClearTableData<AuthorEntity>();

//应用全局过滤器
BasicOrm.Orm.GlobalFilter.Apply<AuthorEntity>("filter01", a => a.IsDeleted);

var author1 = new AuthorEntity();
author1.Name = "李白";
author1.IsDeleted = true;
var author2 = new AuthorEntity();
author2.Name = "王维";

author1 = author1.Insert();
author2 = author2.Insert();

var list = BasicOrm.Orm.Select<AuthorEntity>().ToList();