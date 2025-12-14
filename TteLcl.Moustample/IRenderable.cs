/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TteLcl.Moustample;

/// <summary>
/// An element in a template that can be rendered to a <see cref="TextWriter"/>
/// given a <see cref="TemplateRenderContext"/>.
/// </summary>
public interface IRenderable
{
  /// <summary>
  /// Render this object to text and write it to <paramref name="writer"/>
  /// </summary>
  /// <param name="context">
  /// The context providing the information needed to render the object.
  /// </param>
  /// <param name="writer"></param>
  void Render(TemplateRenderContext context, TextWriter writer);
}

