using System;
using Newtonsoft.Json.Linq;

namespace NetworkApi.Core {

  public static class Extensions {


    /// <summary>
    /// Convert this to any object
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static object ToAnyObject(this JToken token) {
      object result = null;

      switch (token.Type) {
        case JTokenType.Boolean:
          result = token.ToObject<bool>();
          break;
        case JTokenType.String:
          result = token.ToObject<string>();
          break;
        case JTokenType.Date:
          result = token.ToObject<DateTime>();
          break;
        case JTokenType.Float:
          result = token.ToObject<float>();
          break;
        case JTokenType.Integer:
          result = token.ToObject<int>();
          break;
        default: 
          result = token;
          break;
      }

      return result;
    }
  }

}