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
  /// <paramref name="parts"/>. Note that trimming operations
  /// are not perfomed, it is assumed they already were.
  /// </summary>
  /// <param name="parts"></param>
  public Template(IEnumerable<TemplatePart> parts)
    : this()
  {
    _parts.AddRange(parts);
  }

  /// <summary>
  /// Create a new <see cref="Template"/> by parsing the preloaded buffer
  /// </summary>
  /// <param name="buffer"></param>
  /// <returns></returns>
  public static Template Parse(TemplateParseBuffer buffer)
  {
    return buffer.Build();
  }

  /// <summary>
  /// Parse the template text into a new <see cref="Template"/> instance
  /// </summary>
  /// <param name="templateText"></param>
  /// <returns></returns>
  public static Template Parse(string templateText)
  {
    var buffer = new TemplateParseBuffer(templateText);
    return buffer.Build();
  }

  /// <summary>
  /// A read-only view on the sequence of parts in this template
  /// </summary>
  public IReadOnlyList<TemplatePart> Parts => _parts;

  /// <summary>
  /// Append another part, and apply any pending trimming operations.
  /// </summary>
  internal void AddPartAndTrim(TemplatePart part)
  {
    var precedingPart = _parts.LastOrDefault();
    if(part is TrimPart trimmer)
    {
      if(trimmer.TrimPreceding && precedingPart is PlainPart pp)
      {
        // replace existing plain part
        _parts[_parts.Count - 1] = pp.TrimTail();
      }
    }
    else if(part is PlainPart plainPart)
    {
      if(precedingPart is TrimPart tp && tp.TrimFollowing)
      {
        part = plainPart.TrimHead();
      }
    }
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
