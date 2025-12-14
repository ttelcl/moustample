/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace TteLcl.Moustample.EvaluationModel;

/// <summary>
/// A scalar data value (in the current implementation: a string)
/// </summary>
public class ScalarValue: DataValue
{
  /// <summary>
  /// Create a new ScalarValue
  /// </summary>
  public ScalarValue(string value)
  {
    AsScalar = value;
  }

  /// <summary>
  /// Convert an integer to a string and wraps it as a <see cref="ScalarValue"/>
  /// </summary>
  /// <param name="value"></param>
  public ScalarValue(long value)
    : this(value.ToString(CultureInfo.InvariantCulture))
  {
  }

  /// <summary>
  /// Convert a floating point value to a string and wrap it as a <see cref="ScalarValue"/>
  /// </summary>
  /// <param name="value"></param>
  public ScalarValue(double value)
    : this(value.ToString(CultureInfo.InvariantCulture))
  {
  }

  /// <summary>
  /// Try to convert a scalar <see cref="JValue"/> to a <see cref="ScalarValue"/>.
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public static ScalarValue From(JValue value)
  {
    return value.Type switch {
      JTokenType.String => new ScalarValue((string)value!),
      JTokenType.Integer => new ScalarValue((long)value),
      JTokenType.Float => new ScalarValue((double)value),
      JTokenType.Null => ScalarValue.Empty,
      JTokenType.Boolean => new ScalarValue(((bool)value).ToString(CultureInfo.InvariantCulture)),
      JTokenType.Guid => new ScalarValue(((Guid)value).ToString()),
      _ => new ScalarValue(value.ToString()), // YOLO :)
    };
  }

  /// <summary>
  /// A prebuilt scalar value with the empty string as its value
  /// </summary>
  public static ScalarValue Empty { get; } = new ScalarValue(String.Empty);

  /// <inheritdoc/>
  public override string AsScalar { get; }

  /// <summary>
  /// Always returns <see cref="Empty"/>
  /// </summary>
  public override DataValue this[int index] { get => Empty; }

  /// <summary>
  /// Always returns <see cref="Empty"/>
  /// </summary>
  public override DataValue this[string key] { get => Empty; }

}
