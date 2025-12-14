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
/// A <see cref="TemplatePart"/> carrying an instruction for generating
/// content in some way (anything from a simple parameter lookup to more complicated cases)
/// </summary>
public class InstructionPart: TrimPart
{
  /// <summary>
  /// Create a new InstructionPart
  /// </summary>
  public InstructionPart(PlainPartTrimming trimming, ReadOnlyMemory<char> content)
    : base(trimming)
  {
    if(content.IsEmpty)
    {
      // set content but skip any further processing
      Content = content;
    }
    else
    {
      var span = content.Span;
      if(span[0] == '-' || span[^1] == '-')
      {
        throw new ArgumentException(
          "this constructor expects trimming markers to have been processed already");
      }
      Content = content.Trim();
    }
  }

  /// <summary>
  /// Create a new <see cref="InstructionPart"/> from a content slice, parsing
  /// and removing any trim markers and trimming the remaining internal content.
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  public static InstructionPart FromContent(ReadOnlyMemory<char> content)
  {
    if(content.IsEmpty)
    {
      return new InstructionPart(PlainPartTrimming.NoTrimming, content);
    }
    else
    {
      var span = content.Span;
      if(content.Length == 1 && span[0] == '-')
      {
        // special case: trim on both sides (even though there is only one '-')
        return new InstructionPart(PlainPartTrimming.TrimBoth, ReadOnlyMemory<char>.Empty);
      }
      else
      {
        var trimming = PlainPartTrimming.NoTrimming;
        if(span[0] == '-')
        {
          trimming |= PlainPartTrimming.TrimPreceding;
          content = content[1..];
        }
        if(span[^1] == '-')
        {
          trimming |= PlainPartTrimming.TrimFollowing;
          content = content[..^1];
        }
        return new InstructionPart(trimming, content);
      }
    }
  }

  /// <summary>
  /// The raw content of this instruction. Unparsed, except for extracting the trimming
  /// flags and trimming leading and trailing content whitespace
  /// </summary>
  public ReadOnlyMemory<char> Content { get; }

  /// <inheritdoc/>
  protected override void Render(TemplateRenderContext context, TextWriter writer)
  {
    // TEMPORARY
    writer.Write("{");
    writer.Write(TrimPreceding ? '-' : '+');
    writer.Write("<DEBUG>{");
    writer.Write(Content);
    writer.Write("}</DEBUG>");
    writer.Write(TrimFollowing ? '-' : '+');
    writer.Write("}");
  }

}
