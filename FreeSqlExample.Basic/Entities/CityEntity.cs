using FreeSql.DataAnnotations;

namespace FreeSqlExample.Basic.Entities
{
    [Table(Name = nameof(CityEntity))]
    public class CityEntity
    {
        [Navigate(nameof(ParentID))]
        public List<CityEntity> Children { get; set; }

        [Column(Position = 1, IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; } // 主键

        [Column(Position = 2)]
        public string Name { get; set; } // 城市名称

        [Navigate(nameof(ParentID))]
        public CityEntity Parent { get; set; }

        [Column(Position = 4)]
        public int? ParentID { get; set; } // 父级城市的ID，对于顶级城市（如省份），这个字段可以是 null

        [Column(Position = 3)]
        public int Population { get; set; } // 人口数量
    }
}