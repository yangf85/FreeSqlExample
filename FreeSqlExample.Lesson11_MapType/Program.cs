using FreeSql;
using FreeSql.Internal.Model;
using FreeSqlExample.Basic;
using FreeSqlExample.Basic.Entities;
using Newtonsoft.Json;

namespace FreeSqlExample.Lesson11_MapType
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BaseEntity.Initialization(BasicOrm.Orm, null);//初始化需要放在同步表结构之前操作，否则报错id不能为null

            FreeSql.Internal.Utils.TypeHandlers.TryAdd(typeof(List<int>), new ListToStringTypeHandler<int>());
            FreeSql.Internal.Utils.TypeHandlers.TryAdd(typeof(List<string>), new ListToStringTypeHandler<string>());
            FreeSql.Internal.Utils.TypeHandlers.TryAdd(typeof(List<MaterialSource>), new ListToStringTypeHandler<MaterialSource>());
            var product = new ProductEntity()
            {
                Name = "散热器",
                Materials = new List<MaterialSource>() { MaterialSource.Self, MaterialSource.Other, MaterialSource.Customer },
                ExampleList1 = new List<string>() { "A", "B", "C", "D" },
                ExampleList2 = new List<int>() { 1, 2, 3, 4, }
            };

            BasicOrm.Orm.Insert(product).ExecuteAffrows();

            var p1 = BasicOrm.Orm.Select<ProductEntity>().ToList().Last();
        }
    }

    public class ListToStringTypeHandler<T> : TypeHandler<List<T>>
    {
        public override List<T> Deserialize(object value)
        {
            return JsonConvert.DeserializeObject<List<T>>((string)value);
        }

        public override object Serialize(List<T> value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}