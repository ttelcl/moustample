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
  public PlainPart(ReadOnlyMemory<char> plainText)
  {
    PlainText = plainText;
  }

  /// <summary>
  /// The plain content of this part
  /// </summary>
  public ReadOnlyMemory<char> PlainText { get; }

  /// <summary>
  /// Return a <see cref="PlainPart"/> containing this part's content, 
  /// but with trailing whitespace down to and including the final
  /// line break removed.
  /// </summary>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public TemplatePart TrimTail()
  {
    var m = PlainText;
    var span = m.Span;
    while(span.Length > 0 && Char.IsWhiteSpace(span[^1]))
    {
      var ch = span[^1];
      if(ch == '\n' || ch == '\r')
      {
        break;
      }
      span = span[..^1];
    }
    if(span.Length > 0)
    {
      var ch = span[^1];
      if(ch == '\r')
      {
        span = span[..^1];
      }
      else if(ch == '\n')
      {
        span = span[..^1];
        if(span.Length > 0 && span[^1]=='\r')
        {
          span = span[..^1];
        }
      }
    }
    m = m[..span.Length];
    if(m.Length == PlainText.Length)
    {
      return this;
    }
    else
    {
      return new PlainPart(m);
    }
  }

  /// <summary>
  /// Return a <see cref="PlainPart"/> containing this part's content, 
  /// but with leading whitespace up to and including the first
  /// line break removed.
  /// </summary>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public TemplatePart TrimHead()
  {
    var m = PlainText;
    var span = m.Span;
    while(span.Length > 0 && Char.IsWhiteSpace(span[0]))
    {
      var ch = span[0];
      if(ch == '\n' || ch == '\r')
      {
        break;
      }
      span = span[1..];
    }
    if(span.Length > 0)
    {
      var ch = span[0];
      if(ch == '\n')
      {
        span = span[1..];
      }
      else if(ch == '\r')
      {
        span = span[1..];
        if(span.Length > 0 && span[0]=='\n')
        {
          span = span[1..];
        }
      }
    }
    m = m[(m.Length - span.Length) ..];
    if(m.Length == PlainText.Length)
    {
      return this;
    }
    else
    {
      return new PlainPart(m);
    }
  }

  /// <inheritdoc/>
  protected override void Render(TemplateRenderContext context, TextWriter writer)
  {
    writer.Write(PlainText.Span);
  }
}
