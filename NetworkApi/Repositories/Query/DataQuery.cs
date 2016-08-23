


using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetworkApi.Repositories.Query {


  /// <summary>
  /// Data query class 
  /// </summary>
  public class DataQuery {
    
    /// <summary>
    /// the condition of the query
    /// </summary>
    public Condition Condition { get; set;}


    /// <summary>
    /// and operator
    /// </summary>
    const string AndOperator = "$and";

    /// <summary>
    /// or operator
    /// </summary>
    const string OrOperator = "$or";


    const string EqualsOperator = "$equals";

    const string GreaterOperator = "$gt";

    const string LessOperator = "$lt";

    const string GreaterOrEqualsOperator = "$gte";

    const string LessOrEqualsOperator = "$lte";

    const string ContainsOperator = "$contains";


    /// <summary>
    /// Create a data query
    /// </summary>
    public DataQuery(string query)
          :this(JsonConvert.DeserializeObject<JObject>(query)) {
    }


    /// <summary>
    /// create a dataquery from a JObject query
    /// </summary>
    /// <param name="query"></param>
    public DataQuery(JObject query) {
      var properties = query.Properties();
      Condition = ParseQuery(query, LogicOperator.And);
    }

    /// <summary>
    /// Create a condition from a query
    /// </summary>
    /// <param name="query"></param>
    /// <param name="logicOperator"></param>
    /// <returns></returns>
    private Condition ParseQuery(JObject query, LogicOperator logicOperator)
    {
      var result = new LogicCondition(logicOperator);
      var properties = query.Properties();

      foreach (var property in properties) {
        var name = property.Name;

        if (name == AndOperator) {
          var condition = ParseQuery(property.Value as JObject, LogicOperator.And);
          result.Conditions.Add(condition);
        } else if (name == OrOperator) {
          var condition = ParseQuery(property.Value as JObject, LogicOperator.Or);
          result.Conditions.Add(condition);
        } else {
          var conditions = ParseFieldConditions(property);

          foreach (var condition in conditions) {
            result.Conditions.Add(condition);
          }
        }
      }

      return result;
    }

    /// <summary>
    /// parse field condition
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    private IList<Condition> ParseFieldConditions(JProperty property)
    {
        var result = new List<Condition>();

        /// parse list of object
        if (property.Value.Type == JTokenType.Object) {
          var filterData = property.Value as JObject;

          foreach (var filter in filterData.Properties()) {
            var theOperator = Operator.Equals;

            switch (filter.Name) {
              case GreaterOperator:
                theOperator = Operator.Greater;
                break;
              case LessOperator:
                theOperator = Operator.Less;
                break;
              case GreaterOrEqualsOperator:
                theOperator = Operator.GreaterOrEquals;
                break;
              case LessOrEqualsOperator:
                theOperator = Operator.LessOrEquals;
                break;
              case ContainsOperator:
                theOperator = Operator.Contains;
                break;
            }
            
            var condition = new ExpressionCondition(
              theOperator, property.Name, filter.Value
            );

            result.Add(condition);
          }

        } else {
          var condition = new ExpressionCondition(
            Operator.Equals,
            property.Name,
            property.Value
          );

          result.Add(condition);
        }

        return result;
    }
  }
}