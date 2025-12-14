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
/// A part in a <see cref="Template"/>.
/// </summary>
public abstract class TemplatePart: IRenderable
{
  /// <summary>
  /// Create a new TemplatePart
  /// </summary>
  protected TemplatePart()
  {
  }

  /// <summary>
  /// Create a new plain text part
  /// </summary>
  public static TemplatePart PlainTextPart(string text)
  {
    return new PlainPart(text);
  }

  /// <summary>
  /// Renders this part to the given <paramref name="writer"/>.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="writer"></param>
  protected abstract void Render(TemplateRenderContext context, TextWriter writer);

  void IRenderable.Render(TemplateRenderContext context, TextWriter writer)
  {
    Render(context, writer);
  }
}
