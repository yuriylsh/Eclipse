namespace QuickSort.Tests

open QuickSort.PivotIndexSelector
open QuickSort.QuickSorter
open Xunit
open System.IO

module PivotTests =
    
    [<Fact>]
    let `` pivot when given array with one item returns the item's index as final pivot index``() =
        let input = [|10|]
        let actualPivotFinalPosition = pivot pivotIndexSelectorFirstElement input EntireArray
        let expectedPivotFinalPosition = 0
        Assert.Equal(expectedPivotFinalPosition, actualPivotFinalPosition)

    [<Fact>]
    let `` pivot when given array with two sorted items returns the first item index and keep array sorted``() =
        let input = [|10; 20|]
        let actualPivotFinalPosition = pivot pivotIndexSelectorFirstElement input EntireArray
        let expectedPivotFinalPosition = 0
        Assert.Equal(expectedPivotFinalPosition, actualPivotFinalPosition)
        Assert.Equal(10, input.[0])
        Assert.Equal(20, input.[1])

    [<Fact>]
    let `` pivot when given array with two reverse sorted items returns the last item index and swaps items``() =
        let input = [|20; 10|]
        let actualPivotPosition = pivot pivotIndexSelectorFirstElement input EntireArray
        let expectedPivotPosition = 1
        Assert.Equal(expectedPivotPosition, actualPivotPosition)
        Assert.Equal(10, input.[0])
        Assert.Equal(20, input.[1])

    [<Fact>]
    let `` pivot when given example array returns follows the example`` () =
        let input = [|3; 8; 2; 5; 1; 4; 7; 6|]
        let actual = pivot pivotIndexSelectorFirstElement input EntireArray
        Assert.Equal(2, actual)
        Assert.Equal(1, input.[0])
        Assert.Equal(2, input.[1])
        Assert.Equal(3, input.[2])
        Assert.Equal(5, input.[3])
        Assert.Equal(8, input.[4])
        Assert.Equal(4, input.[5])
        Assert.Equal(7, input.[6])
        Assert.Equal(6, input.[7])