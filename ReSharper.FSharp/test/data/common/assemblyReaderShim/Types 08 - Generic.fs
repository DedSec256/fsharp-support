module Module

typeof<Foo<_>> |> ignore
typeof<Foo<int>> |> ignore
typeof<Foo<Foo<_>>> |> ignore

Foo() |> ignore
Foo<_>() |> ignore
Foo<int>() |> ignore
Foo<Foo<_>>() |> ignore