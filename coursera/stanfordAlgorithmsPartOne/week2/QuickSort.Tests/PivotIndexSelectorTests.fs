namespace QuickSort.Tests

open QuickSort.PivotIndexSelector
open Xunit

module PivotIndexSelectorTests =
    [<Fact>]
    let ``  pivotIndexSelectorFirstElement when given an array with entire array range returns first element index``() =
        let input = [|10; 20|]
        let actual = pivotIndexSelectorFirstElement input EntireArray
        let expected = 0
        Assert.Equal(expected, actual)

    [<Fact>]
    let `` pivotIndexSelectorFirstElement when given an array with custom range returns the range's start index`` () =
        let input = [|10; 20; 30; 40; 50|]
        let actual = pivotIndexSelectorFirstElement input (BetweenIndexes(1, 3))
        let expected = 1
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``  pivotIndexSelectorLastElement when given an array with entire array range swaps first and last and returns first element index``() =
        let input = [|10; 20; 30|]
        let actual = pivotIndexSelectorLastElement input EntireArray
        let expected = 0
        Assert.Equal(expected, actual)
        Assert.Equal(30, input.[0])
        Assert.Equal(20, input.[1])
        Assert.Equal(10, input.[2])
    
    [<Fact>]
    let `` pivotIndexSelectorLastElement when given two-item array swaps them and returns the first element index`` () =
        let input = [|10; 20|]
        let actual = pivotIndexSelectorLastElement input EntireArray
        let expected = 0
        Assert.Equal(expected, actual)
        Assert.Equal(20, input.[0])
        Assert.Equal(10, input.[1])
   
    [<Fact>]
    let `` pivotIndexSelectorLastElement when given one-element array returns 0`` () =
        let input = [|10|]
        let actual = pivotIndexSelectorLastElement input EntireArray
        Assert.Equal(0, actual)
        Assert.Equal(10, input.[0])

    [<Fact>]
    let `` pivotIndexSelectorMedianElement when given single-element array returns 0`` () =
        let actual = pivotIndexSelectorMedianElement [|10|] EntireArray
        Assert.Equal(0, actual)

    
    [<Fact>]
    let `` pivotIndexSelectorMedianElement when given two-element array returns first element's index`` () =
        let input = [|20; 10|]
        let actual = pivotIndexSelectorMedianElement input EntireArray
        Assert.Equal(0, actual)
        Assert.Equal(20, input.[0])
        Assert.Equal(10, input.[1])

    [<Fact>]
    let `` pivotIndexSelectorMedianElement when given odd-number array returns correct index`` () =
        let input = [|-1; 30; 10; 20; -2|]
        let actual = pivotIndexSelectorMedianElement input (BetweenIndexes(1, 3))
        Assert.Equal(1, actual)
        Assert.Equal(20, input.[1])
        Assert.Equal(10, input.[2])
        Assert.Equal(30, input.[3])

    [<Fact>]
    let `` pivotIndexSelectorMedianElement when given even-number array returns correct index`` () =
        let input = [|0;1;2;3;4;5;6;7|]
        let actual = pivotIndexSelectorMedianElement input EntireArray
        Assert.Equal(0, actual)
        Assert.Equal(3, input.[0])
        Assert.Equal(0, input.[3])