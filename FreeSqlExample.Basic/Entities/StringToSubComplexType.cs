using FreeSql.Internal.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeSqlExample.Basic.Entities
{
    public class StringToSubComplexType : TypeHandler<SubComplexType>
    {
        public override SubComplexType Deserialize(object value)
        {
            return JsonConvert.DeserializeObject<SubComplexType>((string)value);
        }

        public override object Serialize(SubComplexType value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}