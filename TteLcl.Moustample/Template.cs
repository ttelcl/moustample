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

using TteLcl.Moustample.TemplateModel;

namespace TteLcl.Moustample;

/// <summary>
/// The in-memory representation of a template. Modeled as a
/// sequence of zero or more <see cref="TemplatePart"/>s.
/// </summary>
public class Template: IRenderable
{
  private readonly List<TemplatePart> _parts;

  /// <summary>
  /// Create a new empty <see cref="Template"/>
  /// </summary>
  internal Template()
  {
    _parts = [];
  }

  /// <summary>
  /// Create a new <see cref="Template"/> containing the given
  /// <paramref name="parts"/>.
  /// </summary>
  /// <param name="parts"></param>
  public Template(IEnumerable<TemplatePart> parts)
    : this()
  {
    _parts.AddRange(parts);
  }

  /// <summary>
  /// A read-only view on the sequence of parts in this template
  /// </summary>
  public IReadOnlyList<TemplatePart> Parts => _parts;

  /// <summary>
  /// Append another part
  /// </summary>
  internal void AddPart(TemplatePart part)
  {
    _parts.Add(part);
  }

  /// <inheritdoc/>
  public void Render(TemplateRenderContext context, TextWriter writer)
  {
    foreach(IRenderable part in _parts)
    {
      part.Render(context, writer);
    }
  }
}
