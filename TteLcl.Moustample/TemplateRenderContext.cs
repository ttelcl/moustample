/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TteLcl.Moustample.TemplateModel;

namespace TteLcl.Moustample;

/// <summary>
/// The context containing everything needed to render
/// <see cref="TemplatePart"/>s to output text.
/// </summary>
public class TemplateRenderContext
{
  /// <summary>
  /// Create a new TemplateRenderContext
  /// </summary>
  public TemplateRenderContext()
  {
  }

  /// <summary>
  /// Debug aid: when true, do not interpret instructions, only break the
  /// template into parts
  /// </summary>
  public bool PreparseOnly { get; set; }

}
