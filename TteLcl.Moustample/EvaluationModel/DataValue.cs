/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace TteLcl.Moustample.EvaluationModel;

/// <summary>
/// Values of data items. The content can be of a few different types,
/// and depending on that type there is a natural way to make use of the
/// value. All values support all ways of using the data (even though many
/// combinations may make no sense). The ways of access are: scalar,
/// indexed, mapped.
/// </summary>
public abstract class DataValue
{
  /// <summary>
  /// Create a new DataValue
  /// </summary>
  protected DataValue()
  {
  }

  /// <summary>
  /// Wrap a string as a <see cref="DataValue"/>
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public static ScalarValue From(string value)
  {
    return new ScalarValue(value);
  }

  /// <summary>
  /// Convert an integer to a string and wrap it as a <see cref="DataValue"/>
  /// </summary>
  public static ScalarValue From(long value)
  {
    return new ScalarValue(value);
  }

  /// <summary>
  /// Convert a floating point value to a string and wrap it as a <see cref="DataValue"/>
  /// </summary>
  public static ScalarValue From(double value)
  {
    return new ScalarValue(value);
  }

  /// <summary>
  /// Wrap a list of <see cref="DataValue"/>s as a <see cref="ListValue"/>
  /// </summary>
  public static ListValue From(IEnumerable<DataValue> values)
  {
    return new ListValue(values);
  }

  /// <summary>
  /// Wrap a dictionary from strings to <see cref="DataValue"/> as a <see cref="MapValue"/>
  /// </summary>
  public static MapValue From(IReadOnlyDictionary<string, DataValue> map, IEqualityComparer<string> comparer)
  {
    return new MapValue(map, comparer);
  }

  /// <summary>
  /// Wrap a dictionary from strings to <see cref="DataValue"/> as a <see cref="MapValue"/>
  /// </summary>
  public static MapValue From(IReadOnlyDictionary<string, DataValue> map, bool caseSensitive)
  {
    return new MapValue(map, caseSensitive);
  }

  /// <summary>
  /// Wrap a dictionary from strings to <see cref="DataValue"/> as a case sensitive <see cref="MapValue"/>
  /// </summary>
  public static MapValue From(IReadOnlyDictionary<string, DataValue> map)
  {
    return new MapValue(map, true);
  }

  /// <summary>
  /// Convert a <see cref="JToken"/> (i.e. anything from JSON) into a <see cref="DataValue"/>.
  /// </summary>
  /// <param name="value">
  /// The value to convert (recursively)
  /// </param>
  /// <param name="comparer">
  /// The comparer that determines case sensitivity of any objects in the value tree
  /// </param>
  /// <returns></returns>
  /// <exception cref="NotSupportedException"></exception>
  public static DataValue From(JToken value, IEqualityComparer<string> comparer)
  {
    if(value is JValue jValue)
    {
      return ScalarValue.From(jValue);
    }
    switch(value.Type)
    {
      case JTokenType.Object:
        return new MapValue((JObject)value, comparer);
      case JTokenType.Array:
        return new ListValue(((JArray)value).Select(v2 => From(v2, comparer)));
      default:
        throw new NotSupportedException(
          $"Unsupported JSON type: {value.Type}");
    }
  }

  /// <summary>
  /// Return the value as a scalar string.
  /// If not applicable: return <see cref="String.Empty"/>
  /// </summary>
  public abstract string AsScalar { get; }

  /// <summary>
  /// Return an integer-indexed DataValue
  /// If not applicable: return <see cref="ScalarValue.Empty"/>
  /// </summary>
  public abstract bool TryGetValue(int index, out DataValue value);

  /// <summary>
  /// Return an string-indexed scalar string
  /// If not applicable: return <see cref="ScalarValue.Empty"/>
  /// </summary>
  public abstract bool TryGetValue(string key, out DataValue value);



}
