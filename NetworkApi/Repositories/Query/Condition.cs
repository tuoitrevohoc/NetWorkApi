

using System.Collections.Generic;

namespace NetworkApi.Repositories.Query {

  /// <summary>
  /// the base class for condition
  /// </summary>
  public abstract class Condition {
  }

  /// <summary>
  /// Operator of a Logic Condition
  /// </summary>
  public enum LogicOperator {
    And,
    Or
  }

  /// <summary>
  /// /// Operator of a Expressed Condition
  /// </summary>
  public enum Operator {
    Equals,
    Greater,
    Less,
    GreaterOrEquals,
    LessOrEquals,
    Contains,
    In,
  }

  /// <summary>
  /// base class for logical condition like and or or
  /// </summary>
  public class LogicCondition: Condition {

    /// <summary>
    /// The operator 
    /// </summary>
    /// <returns></returns>
    public LogicOperator Operator { get; set; }

    /// <summary>
    /// List of condition
    /// </summary>
    /// <returns></returns>
    public IList<Condition> Conditions { get; set; }

    /// <summary>
    /// Logic operator
    /// </summary>
    /// <param name="logicOperator"></param>
    public LogicCondition(LogicOperator logicOperator = LogicOperator.And) {
      this.Operator = logicOperator;
      Conditions = new List<Condition>();
    }
  }


  /// <summary>
  /// Expression conditions
  /// </summary>
  public class ExpressionCondition: Condition {

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Operator Operator { get; set; }

    /// <summary>
    /// field name
    /// </summary>
    /// <returns></returns>
    public string FieldName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public object Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="anOperator"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    public ExpressionCondition(Operator anOperator, string fieldName, object value) {
      this.Operator = anOperator;
      this.FieldName = fieldName;
      this.Value = value;
    }

  }
}