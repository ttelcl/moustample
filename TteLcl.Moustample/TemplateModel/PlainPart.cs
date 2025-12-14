/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TteLcl.Moustample.TemplateModel;

/// <summary>
/// A <see cref="TemplatePart"/> that contain just plain text
/// </summary>
public class PlainPart: TemplatePart
{
  /// <summary>
  /// Create a new PlainPart
  /// </summary>
  public PlainPart(string plainText)
  {
    PlainText = plainText;
  }

  /// <summary>
  /// The plain content of this part
  /// </summary>
  public string PlainText { get; }

  /// <inheritdoc/>
  protected override void Render(TemplateRenderContext context, TextWriter writer)
  {
    writer.Write(PlainText);
  }
}
