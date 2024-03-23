using FreeSql.Internal.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace FreeSqlExample.Basic.Entities
{
    /// <summary>
    /// 材料供应方式
    /// </summary>
    public enum MaterialSource
    {
        [Description("公司采购")]
        Self,

        [Description("客户自带")]
        Customer,

        [Description("外协采购")]
        Outsource,

        [Description("库存")]
        Stock,

        [Description("其他")]
        Other,
    }
}