using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPublish
{
    class JsonDateConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            double miliseconds = Convert.ToDouble(reader.Value);
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(miliseconds);
        }
    }
}
