/*
 * (c) 2025  ttelcl / ttelcl
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TteLcl.Moustample.EvaluationModel;

/// <summary>
/// Static utilities related to identifiers
/// </summary>
public static partial class Identifiers
{
  private static readonly Regex __identifierPrefixRegex = IdentifierPrefixRegex();

  /// <summary>
  /// Split the <paramref name="candidate"/> string into a leading identifier
  /// and the rest
  /// </summary>
  /// <param name="candidate">
  /// The candidate string to split
  /// </param>
  /// <param name="identifier">
  /// The initial identifier (an empty string if <paramref name="candidate"/>
  /// did not start with a valid identifier)
  /// </param>
  /// <param name="rest">
  /// The remainder of the <paramref name="candidate"/> after <paramref name="identifier"/>
  /// (a copy of <paramref name="candidate"/> if it did not start with a valid identifier)
  /// </param>
  /// <returns></returns>
  public static bool TryPeelIdentifier(
    string candidate, out string identifier, out string rest)
  {
    var m = __identifierPrefixRegex.Match(candidate);
    if(m.Success)
    {
      identifier = m.Value;
      rest = candidate[identifier.Length..];
      return true;
    }
    else
    {
      identifier = String.Empty;
      rest = candidate;
      return false;
    }
  }

  /// <summary>
  /// Test if <paramref name="candidate"/> is a valid identifier
  /// string for moustample.
  /// </summary>
  public static bool IsIdentifier(string candidate)
  {
    var m = __identifierPrefixRegex.Match(candidate);
    return m.Success && m.Length == candidate.Length;
  }

  [GeneratedRegex(@"^[a-z_][a-z_0-9]*(-[a-z_0-9]+)*", RegexOptions.IgnoreCase)]
  private static partial Regex IdentifierPrefixRegex();
}
