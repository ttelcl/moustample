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

namespace TteLcl.Moustample.EvaluationModel;

/// <summary>
/// Stores the result of parsing a raw instruction
/// </summary>
public abstract class PreparedInstruction: IRenderable
{
  /// <summary>
  /// Create a new PreparedInstruction
  /// </summary>
  protected PreparedInstruction()
  {
  }

  /// <summary>
  /// Parse the instruction
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public static PreparedInstruction Parse(ReadOnlyMemory<char> content)
  {
    throw new NotImplementedException(
      "Instruction parsing is NYI (use -preparse to only run the template toplevel parser)");
  }

  /// <summary>
  /// Evaluate the prepared instruction and render the result
  /// </summary>
  protected abstract void Render(TemplateRenderContext context, TextWriter writer);

  /// <summary>
  /// Evaluate the prepared instruction and render the result
  /// </summary>
  void IRenderable.Render(TemplateRenderContext context, TextWriter writer)
  {
    Render(context, writer);
  }
}
