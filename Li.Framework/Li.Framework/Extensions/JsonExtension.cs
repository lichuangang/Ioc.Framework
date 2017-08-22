using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Extensions
{
    /* ==============================================================================
     * 描述：JsonExtension
     * 创建人：李传刚 2017/7/20 16:50:25
     * ==============================================================================
     */
    public static class JsonExtension
    {
        public static T Deserialize<T>(this string value) where T : class
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string Serialize<T>(this T obj) where T : class
        {
            return obj == null ? null : JsonConvert.SerializeObject(obj);
        }

        public static object Deserialize(this string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type);
        }
    }
}
