using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace mobileAPI.BussinessLogic
{
    public class JSONValuFormatter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            try
            {
                if (typeToConvert.FullName == "System.Int32")
                    return (T)Convert.ChangeType(reader.GetInt32(), typeof(Int32));

                //return reader.GetString().ToString();

                return (T)Convert.ChangeType(reader.GetString(), typeof(T));
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            try
            {
                //if (typeof(T).Equals(typeof(decimal?)))
                //    writer.WriteNumberValue(Convert.ToDecimal(value));

                //if (typeof(T).Equals(typeof(decimal)))
                //    writer.WriteNumberValue(Convert.ToDecimal(value));

                //if (typeof(T).Equals(typeof(string)))
                writer.WriteStringValue(value.ToString());

                //if (typeof(T).Equals(typeof(int)))
                //    writer.WriteNumberValue(Convert.ToInt32(value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


