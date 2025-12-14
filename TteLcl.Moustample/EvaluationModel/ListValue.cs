/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TteLcl.Moustample.EvaluationModel;

/// <summary>
/// Implements the indexed variant of <see cref="DataValue"/>
/// </summary>
public class ListValue: DataValue
{
  private readonly List<DataValue> _values;

  /// <summary>
  /// Create a new ListValue
  /// </summary>
  public ListValue(
    IEnumerable<DataValue> values)
  {
    _values = values.ToList();
  }

  /// <summary>
  /// Always returns <see cref="ScalarValue.Empty"/>
  /// </summary>
  public override string AsScalar { get => String.Empty; }

  /// <summary>
  /// Returns the datavalue at the given index, or <see cref="ScalarValue.Empty"/>
  /// if the index is out of range
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public override DataValue this[int index] {
    get {
      return index >= 0 && index < _values.Count ? _values[index] : ScalarValue.Empty;
    }
  }

  /// <summary>
  /// Tries to convert the key to an integer and tries to use that with the
  /// integer indexer. Returns <see cref="ScalarValue.Empty"/> if not found
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  public override DataValue this[string key] {
    get {
      return Int32.TryParse(key, out var index) ? this[index] : ScalarValue.Empty;
    }
  }
}
