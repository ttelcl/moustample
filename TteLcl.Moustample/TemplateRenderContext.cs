/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TteLcl.Moustample.EvaluationModel;
using TteLcl.Moustample.TemplateModel;

namespace TteLcl.Moustample;

/// <summary>
/// The context containing everything needed to render
/// <see cref="TemplatePart"/>s to output text.
/// </summary>
public class TemplateRenderContext: IDataView
{
  /// <summary>
  /// Create a new TemplateRenderContext
  /// </summary>
  public TemplateRenderContext()
  {
    ViewStack = new DataViewStack();
    AllowMissing = true;
  }

  /// <summary>
  /// Debug aid: when true, do not interpret instructions, only break the
  /// template into parts
  /// </summary>
  public bool PreparseOnly { get; set; }

  /// <summary>
  /// Determines the behaviour of <see cref="GetValue(string)"/>.
  /// If true (default), missing data values are returned as <see cref="ScalarValue.Empty"/>.
  /// If false, an exception is thrown instead.
  /// </summary>
  public bool AllowMissing { get; set; }

  /// <summary>
  /// The data view stack
  /// </summary>
  public DataViewStack ViewStack { get; }

  /// <summary>
  /// Implements <see cref="IDataView"/> through <see cref="ViewStack"/>,
  /// </summary>
  public bool TryGetValue(string key, out DataValue value)
  {
    return ViewStack.TryGetValue(key, out value);
  }

  /// <summary>
  /// Looks up a data value by name and returns it if found.
  /// If not found <see cref="AllowMissing"/> determines what happens:
  /// if that if true, <see cref="ScalarValue.Empty"/> is returned,
  /// otherwise a <see cref="KeyNotFoundException"/> is thrown.
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  /// <exception cref="KeyNotFoundException"></exception>
  public DataValue GetValue(string key)
  {
    if(!TryGetValue(key, out var value) && !AllowMissing)
    {
      throw new KeyNotFoundException(
        $"Missing value '{key}'");
    }
    return value;
  }

}
