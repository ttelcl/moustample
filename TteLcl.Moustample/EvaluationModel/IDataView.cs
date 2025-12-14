/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TteLcl.Moustample.EvaluationModel;

/// <summary>
/// An object that can resolve data lookups
/// </summary>
public interface IDataView
{
  /// <summary>
  /// Look up a data value by name
  /// </summary>
  /// <param name="key">
  /// The key to look up
  /// </param>
  /// <param name="value">
  /// Upon success: the value that was found. Upon failure: <see cref="ScalarValue.Empty"/>
  /// </param>
  /// <returns>
  /// True if the value was found, false if it wasn't
  /// </returns>
  bool TryGetValue(string key, out DataValue value);
}

