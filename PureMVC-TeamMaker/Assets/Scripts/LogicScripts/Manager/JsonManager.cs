using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class JsonManager : IManager
{
    public string Name { get; set; }

    public static T ToClass<T>(string json)
    {
      return JsonConvert.DeserializeObject<T>(json);
    }

    public static string ToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}
