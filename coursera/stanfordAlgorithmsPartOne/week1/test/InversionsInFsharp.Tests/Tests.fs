module Test.Tests

open Xunit
open Library

[<Fact>]    
let ``Library converts "Banana" correctly``() =
    let expected = """Yuriy"""
    let actual = "Yuriy"
    Assert.Equal(expected, actual)