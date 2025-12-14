/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TteLcl.Moustample.TemplateModel;

/// <summary>
/// Buffers the full template text for the parsing logic
/// </summary>
public class TemplateParseBuffer
{
  /// <summary>
  /// Create a new <see cref="TemplateParseBuffer"/>
  /// </summary>
  public TemplateParseBuffer(ReadOnlyMemory<char> templateText)
  {
    TemplateText = templateText;
    ParsePointer = 0;
  }

  /// <summary>
  /// Create a new <see cref="TemplateParseBuffer"/>
  /// </summary>
  public TemplateParseBuffer(string templateText)
    : this(templateText.AsMemory())
  {
  }

  /// <summary>
  /// Parse all parts and combine them into a new <see cref="Template"/>
  /// </summary>
  /// <returns></returns>
  public Template Build()
  {
    var template = new Template();
    foreach(var part in BuildParts())
    {
      template.AddPartAndTrim(part);
    }
    return template;
  }

  /// <summary>
  /// Parse the template content into parts
  /// </summary>
  /// <returns></returns>
  public IEnumerable<TemplatePart> BuildParts()
  {
    while(TryGetNextPart(out var part))
    {
      yield return part;
    }
  }

  /// <summary>
  /// The full template text
  /// </summary>
  public ReadOnlyMemory<char> TemplateText { get; }

  /// <summary>
  /// The current position of the parse pointer
  /// </summary>
  public int ParsePointer { get; private set; }

  /// <summary>
  /// True if parsing is complete (<see cref="ParsePointer"/> points beyond the
  /// end of <see cref="TemplateText"/>).
  /// </summary>
  public bool Done => ParsePointer >= TemplateText.Length;

  /// <summary>
  /// Try to parse the next part
  /// </summary>
  /// <param name="part"></param>
  /// <returns></returns>
  public bool TryGetNextPart([NotNullWhen(true)] out TemplatePart? part)
  {
    var parsePointer0 = ParsePointer;
    var partSpan = TemplateText.Span[ParsePointer..];
    if(partSpan.IsEmpty)
    {
      part = null;
      return false;
    }
    var moustacheCount = CountRepeatedCharacter(partSpan, '{');
    if(moustacheCount >= 2)
    {
      part = ParseInstructionPart(partSpan);
      if(parsePointer0 >= ParsePointer)
      {
        throw new InvalidOperationException("Internal error: parse pointer did not move");
      }
      return true;
    }
    else
    {
      part = ParsePlainPart(partSpan);
      if(parsePointer0 >= ParsePointer)
      {
        throw new InvalidOperationException("Internal error: parse pointer did not move");
      }
      return true;
    }
  }

  private PlainPart ParsePlainPart(ReadOnlySpan<char> span)
  {
    // We know that there is at least one plaintext character in the buffer
    var index = 1;
    while(true)
    {
      while(index < span.Length && span[index] != '{')
      {
        index++;
      }
      var moustacheCount = CountRepeatedCharacter(span[index..], '{');
      if(moustacheCount >= 2)
      {
        var memSlice = TemplateText[ParsePointer..(ParsePointer+index)];
        var part = new PlainPart(memSlice);
        ParsePointer += index;
        return part;
      }
      else if(moustacheCount == 1)
      {
        index += moustacheCount;
        continue;
      }
      else if(moustacheCount == 0)
      {
        // EOF
        var memSlice = TemplateText[ParsePointer..(ParsePointer+index)];
        var part = new PlainPart(memSlice);
        ParsePointer += index;
        return part;
      }
      else
      {
        throw new InvalidOperationException($"Internal error; count = {moustacheCount}");
      }
    }
  }

  private TrimPart ParseInstructionPart(ReadOnlySpan<char> span)
  {
    var outerMoustacheCount = CountRepeatedCharacter(span, '{');
    if(outerMoustacheCount < 2)
    {
      throw new InvalidOperationException($"Expecting count >= 2 but got {outerMoustacheCount}");
    }
    var index = outerMoustacheCount;
    while(true)
    {
      while(index < span.Length && span[index] != '}')
      {
        index++;
      }
      var closeCount = CountRepeatedCharacter(span[index..], '}');
      if(closeCount == 0)
      {
        var target = new String('}', outerMoustacheCount);
        // TODO: map ParsePointer to line and position
        throw new InvalidOperationException(
          $"Unexpected end of template while looking for '{target}' after index {ParsePointer}");
      }
      if(closeCount < outerMoustacheCount)
      {
        index += closeCount;
        // continue;
      }
      else
      {
        var innerTailIndex = ParsePointer + index;
        var outerTailIndex = innerTailIndex + outerMoustacheCount;
        var outerHeadIndex = ParsePointer;
        var innerHeadIndex = outerHeadIndex + outerMoustacheCount;
        var content = TemplateText[innerHeadIndex .. innerTailIndex];
        var part = InstructionPart.FromContent(content);
        ParsePointer = outerTailIndex;
        return part;
      }
    }
  }

  /// <summary>
  /// Count the number of consecutive occurrences of <paramref name="character"/> are found
  /// at the start of <paramref name="buffer"/>
  /// </summary>
  /// <param name="buffer"></param>
  /// <param name="character"></param>
  /// <returns></returns>
  private static int CountRepeatedCharacter(ReadOnlySpan<char> buffer, char character)
  {
    var count = 0;
    while(count < buffer.Length && buffer[count] == character)
    {
      count++;
    }
    return count;
  }
}
