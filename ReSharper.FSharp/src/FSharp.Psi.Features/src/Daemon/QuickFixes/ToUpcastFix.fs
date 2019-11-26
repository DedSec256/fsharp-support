namespace JetBrains.ReSharper.Plugins.FSharp.Psi.Features.Daemon.QuickFixes

open JetBrains.ReSharper.Feature.Services.QuickFixes
open JetBrains.ReSharper.Plugins.FSharp.Psi.Features.Daemon.Highlightings
open JetBrains.ReSharper.Plugins.FSharp.Psi.Impl.Tree
open JetBrains.ReSharper.Plugins.FSharp.Psi.Parsing
open JetBrains.ReSharper.Plugins.FSharp.Psi.Util
open JetBrains.ReSharper.Psi.ExtensionsAPI
open JetBrains.ReSharper.Psi.ExtensionsAPI.Tree
open JetBrains.ReSharper.Psi.Tree
open JetBrains.ReSharper.Resources.Shell
open JetBrains.Util

type ToUpcastFix(error: TypeTestUnnecessaryError) =
    inherit QuickFixBase()

    let expr = error.Expr

    override x.Text = "Replace with upcast"

    override x.IsAvailable _ =
        isValid expr && isValid expr.OperatorToken

    override x.ExecutePsiTransaction(_, _) =
        use writeLock = WriteLockCookie.Create(expr.IsPhysical())

        replaceWithToken expr.OperatorToken FSharpTokenType.COLON_GREATER
        let upcastExpr = ModificationUtil.ReplaceChild(expr, ElementType.UPCAST_EXPR.Create())
        LowLevelModificationUtil.AddChild(upcastExpr, expr.Children().AsArray())

        null