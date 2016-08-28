using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tests
{
    public static class TestExtensions
    {
        public static EntityType Clone<EntityType>(this EntityType instance)
        {
            var json = JsonConvert.SerializeObject(instance);
            return JsonConvert.DeserializeObject<EntityType>(json);
        }
    }
}
