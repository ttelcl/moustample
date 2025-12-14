/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace TteLcl.Moustample.EvaluationModel;

/// <summary>
/// Implements the map version of <see cref="DataValue"/>
/// </summary>
public class MapValue: DataValue
{
  private readonly Dictionary<string, DataValue> _map;

  /// <summary>
  /// Create a new MapValue
  /// </summary>
  public MapValue(IReadOnlyDictionary<string, DataValue> map, bool caseSensitive)
    : this(map, caseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase)
  {
  }

  /// <summary>
  /// Create a new MapValue
  /// </summary>
  public MapValue(IReadOnlyDictionary<string, DataValue> map, IEqualityComparer<string> comparer)
  {
    _map = new Dictionary<string, DataValue>(map, comparer);
  }

  /// <summary>
  /// Create a new MapValue from a <see cref="JObject"/>
  /// </summary>
  /// <param name="job"></param>
  /// <param name="comparer"></param>
  public MapValue(JObject job, IEqualityComparer<string> comparer)
  {
    _map = new Dictionary<string, DataValue>(comparer);
    foreach(var property in job.Properties())
    {
      _map[property.Name] = DataValue.From(property.Value, comparer);
    }
  }

  /// <summary>
  /// The comparer in use by this mapvalue
  /// </summary>
  public IEqualityComparer<string> Comparer => _map.Comparer;

  /// <summary>
  /// Retrieves the value for the empty string: <c>this[""].AsScalar</c>
  /// </summary>
  public override string AsScalar { get => this[String.Empty].AsScalar; }

  /// <summary>
  /// Implemented as <c>this[index.ToString()]</c> (which probably
  /// returns <see cref="ScalarValue.Empty"/>)
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public override DataValue this[int index] { get => this[index.ToString()]; }

  /// <summary>
  /// Returns the value in this map if available. Returns <see cref="ScalarValue.Empty"/>
  /// otherwise
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  public override DataValue this[string key] {
    get {
      return _map.TryGetValue(key, out var value) ? value : ScalarValue.Empty;
    }
  }

  /// <summary>
  /// Set or remove a value
  /// </summary>
  /// <param name="key"></param>
  /// <param name="value"></param>
  public void Set(string key, DataValue? value) {
    if(value == null)
    {
      _map.Remove(key);
    }
    else
    {
      _map[key] = value;
    }
  }
}
