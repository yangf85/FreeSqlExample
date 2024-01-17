// See https://aka.ms/new-console-template for more information

using FreeSql;
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

BaseEntity.Initialization(BasicOrm.Orm, null);//初始化需要放在同步表结构之前操作，否则报错id不能为null
BasicOrm.Orm.CodeFirst.SyncStructure<AuthorEntity>();
BasicOrm.Orm.CodeFirst.SyncStructure<BookEntity>();
BasicOrm.ClearTableData<BookEntity>();
BasicOrm.ClearTableData<AuthorEntity>();

var author1 = new AuthorEntity() { Name = "托尔斯泰" };
var author2 = new AuthorEntity() { Name = "海明威" };

//新增   不会级联更新数据，如果要级联更新需要使用仓储
author1 = author1.Insert();
author2 = author2.Insert();

var book1 = new BookEntity() { Title = "战争与和平", AuthorId = author1.Id };
var book2 = new BookEntity() { Title = "老人与海", AuthorId = author2.Id };
var book3 = new BookEntity() { Title = "丧钟为谁而鸣", AuthorId = author2.Id };

book1 = book1.Insert();
book2 = book2.Insert();
book3 = book3.Insert();

author1.Books = new List<BookEntity>() { book1, };
author2.Books = new List<BookEntity>() { book2, book3 };

//更新
author1.Name = "托尔斯泰2";
author1.Update();

//查询
author1 = AuthorEntity.Find(author1.Id);//没有级联数据
author1 = BasicOrm.Orm.Select<AuthorEntity>().IncludeMany(a => a.Books).First();

var list1 = BookEntity.Select.ToList();//没有级联数据
var list2 = BookEntity.Select.Include(b => b.Author).ToList();

//删除
author2.Delete();//软删除
author2.Restore();//恢复删除
author2.Delete(true);//真删除