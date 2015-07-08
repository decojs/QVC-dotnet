using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Qvc.JsonCamelCase
{
    public class JsonCamelCaseResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);

            jsonProperty.PropertyName = jsonProperty.PropertyName.ToCamelCase();

            return jsonProperty;
        }
    }
}