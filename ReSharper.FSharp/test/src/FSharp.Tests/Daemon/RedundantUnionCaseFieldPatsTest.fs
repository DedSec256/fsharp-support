﻿namespace JetBrains.ReSharper.Plugins.FSharp.Tests.Features.Daemon

open JetBrains.ReSharper.Plugins.FSharp.Psi.Features.Daemon.Highlightings
open JetBrains.ReSharper.Plugins.FSharp.Tests
open JetBrains.ReSharper.Plugins.FSharp.Tests.Features.Daemon
open JetBrains.ReSharper.TestFramework
open NUnit.Framework

[<TestPackages(FSharpCorePackage)>]
type RedundantUnionCaseFieldPatsTest() =
    inherit FSharpHighlightingTestBase()

    override x.RelativeTestDataPath = "features/daemon/redundantUnionCaseFieldPats"

    override x.HighlightingPredicate(highlighting, _, _) =
        highlighting :? RedundantUnionCaseFieldPatternsWarning

    [<Test>] member x.``Active pattern 01``() = x.DoNamedTest()
    [<Test>] member x.``Nested pattern 01``() = x.DoNamedTest()
