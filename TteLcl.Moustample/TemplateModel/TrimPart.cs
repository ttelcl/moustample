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
/// A <see cref="TemplatePart"/> that may affect preceding and following
/// <see cref="PlainPart"/>s, causing them to be trimmed. While this class
/// may be used "as is", it is expected to be mostly appearing as a base class.
/// </summary>
public class TrimPart: TemplatePart
{
  /// <summary>
  /// Create a new TrimPart
  /// </summary>
  public TrimPart(PlainPartTrimming trimming)
  {
    Trimming = trimming;
  }

  /// <summary>
  /// Defines if preceding or subsequent plain parts should be trimmed
  /// </summary>
  public PlainPartTrimming Trimming { get; }

  /// <summary>
  /// True if the preceding plain text part should be trimmed
  /// </summary>
  public bool TrimPreceding => Trimming.HasFlag(PlainPartTrimming.TrimPreceding);

  /// <summary>
  /// True if the following plain text part should be trimmed
  /// </summary>
  public bool TrimFollowing => Trimming.HasFlag(PlainPartTrimming.TrimFollowing);

  /// <inheritdoc/>
  protected override void Render(TemplateRenderContext context, TextWriter writer)
  {
    // do not render anything (but subclasses may)
  }
}
