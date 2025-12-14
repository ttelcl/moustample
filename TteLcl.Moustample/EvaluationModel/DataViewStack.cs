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
/// An implementation of <see cref="IDataView"/> that checks a stack
/// of other <see cref="IDataView"/> implementations for the first one to
/// be able to resolve a request
/// </summary>
public class DataViewStack: IDataView
{
  private readonly Stack<IDataView> _dataViews;

  /// <summary>
  /// Create a new DataViewStack
  /// </summary>
  public DataViewStack()
  {
    _dataViews = new Stack<IDataView>();
  }

  /// <summary>
  /// Push a new view on top of this stack
  /// </summary>
  public void Push(IDataView dataView)
  {
    _dataViews.Push(dataView);
  }

  /// <summary>
  /// Remove the top layer from the stack
  /// </summary>
  public IDataView Pop()
  {
    return _dataViews.Pop();
  }

  /// <inheritdoc/>
  public bool TryGetValue(string key, out DataValue value)
  {
    foreach(var view in _dataViews)
    {
      if(view.TryGetValue(key, out value))
      {
        return true;
      }
    }
    value = ScalarValue.Empty;
    return false;
  }
}
