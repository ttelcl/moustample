/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TteLcl.Moustample.TemplateModel;

/// <summary>
/// Options for trimming neighbouring <see cref="PlainPart"/>s
/// </summary>
[Flags]
public enum PlainPartTrimming
{
  /// <summary>
  /// Do not trim preceding or following plain parts
  /// </summary>
  NoTrimming = 0,

  /// <summary>
  /// Trim the preceding plain part
  /// </summary>
  TrimPreceding = 1,

  /// <summary>
  /// Trim the following plain part
  /// </summary>
  TrimFollowing = 2,

  /// <summary>
  /// Trim on both ends
  /// </summary>
  TrimBoth = TrimPreceding | TrimFollowing,
}
