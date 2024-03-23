// See https://aka.ms/new-console-template for more information
using FreeSql;
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;

BaseEntity.Initialization(BasicOrm.Orm, null);//初始化需要放在同步表结构之前操作，否则报错id不能为null
BasicOrm.ClearTableData<UserEntity>();
BasicOrm.ClearTableData<CompanyEntity>();
BasicOrm.ClearTableData<BookEntity>();

//var user1 = new UserEntity() { Username = "李莫愁" };
//var user2 = new UserEntity() { Username = "小龙女" };
//var user3 = new UserEntity() { Username = "黄蓉" };

//var company1 = new CompanyEntity() { Name = "古墓派" };
//var company2 = new CompanyEntity() { Name = "桃花岛" };

//var rep1 = BasicOrm.Orm.GetRepository<CompanyEntity>();
//var rep2 = BasicOrm.Orm.GetRepository<UserEntity>();

////开启级联保存
//rep1.DbContextOptions.EnableCascadeSave = true;
//rep2.DbContextOptions.EnableCascadeSave = true;

////新增

//#region 新增A

//company1.Users = new List<UserEntity>() { user1, user2 };
//company2.Users = new List<UserEntity>() { user3 };

//company1 = rep1.Insert(company1);
//company2 = rep1.Insert(company2);

//#endregion 新增A

var book1 = new BookEntity();
var book2 = new BookEntity();

var bookRep = BasicOrm.Orm.GetRepository<BookEntity>();

var booka = bookRep.Insert(book1);

booka.Title = "1234";
var bookb = bookRep.Update(booka);

var ss = 1;

#region 新增B

//无法新增 多对一
//user1.Company = company1;
//user1.CompanyId = company1.Id;
//user2.Company = company1;
//user2.CompanyId = company1.Id;
//user3.Company = company2;
//user3.CompanyId = company2.Id;
//rep2.Insert(user1);
//rep2.Insert(user2);
//rep2.Insert(user3);

#endregion 新增B

////更新 只更新变化的属性
//company1 = rep1.Select.First();
//company1.Name = "古墓派1";
//var count = rep1.Update(company1);

////级联删除 返回的是删除的对象和它的级联对象
//var list = rep1.DeleteCascadeByDatabase(c => c.Name.Contains("古墓派"));

//foreach (var item in list)
//{
//    switch (item)
//    {
//        case CompanyEntity company:
//            Console.WriteLine(company.Name);
//            break;

//        case UserEntity user:
//            Console.WriteLine(user.Username);
//            break;

//        default:
//            break;
//    }
//}