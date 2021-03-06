﻿using FSharp.Compiler.SourceCodeServices;
using JetBrains.Annotations;
using JetBrains.Util;

namespace JetBrains.ReSharper.Plugins.FSharp.Psi.Resolve
{
  public class FSharpResolvedSymbolUse
  {
    [NotNull]
    public FSharpSymbolUse SymbolUse { get; }

    public TextRange Range { get; }

    public FSharpResolvedSymbolUse([NotNull] FSharpSymbolUse symbolUse, TextRange range)
    {
      SymbolUse = symbolUse;
      Range = range;
    }
  }
}