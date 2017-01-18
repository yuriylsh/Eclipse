namespace QuickSort.Tests

open QuickSort.PivotIndexSelector
open QuickSort.QuickSorter
open Xunit
open System.IO

module QuickSorterTests =

    [<Fact>]
    let `` quickSort given array with a single item return 0`` () =
        let input = [|10|]
        let actual = quickSort pivotIndexSelectorFirstElement input
        let expected = 0
        Assert.Equal(expected, actual)

    [<Fact>]
    let `` quickSort given two sorted items counts 1 swap and does not change input`` () =
        let input = [|10; 20|]
        let actual = quickSort pivotIndexSelectorFirstElement input
        let expected = 1
        Assert.Equal(expected, actual)
        Assert.Equal(10, input.[0])
        Assert.Equal(20, input.[1])

    [<Fact>]
    let `` quickSort given two reverse sorted items counts 1 swap and sorts input`` () =
        let input = [|20; 10|]
        let actual = quickSort pivotIndexSelectorFirstElement input
        let expected = 1
        Assert.Equal(expected, actual)
        Assert.Equal(10, input.[0])
        Assert.Equal(20, input.[1])

    [<Fact>]
    let `` quickSort given tree items from example counts 3 swaps and sorts input`` () =
        let input =[|8; 7; 6|]
        let actual = quickSort pivotIndexSelectorFirstElement input
        let expected = 3
        Assert.Equal(expected, actual)
        Assert.Equal(6, input.[0])
        Assert.Equal(7, input.[1])
        Assert.Equal(8, input.[2])

    [<Fact>]
    let ``quickSort given  example input counts 15 swaps and sorts input`` () =
        let input = [|3; 8; 2; 5; 1; 4; 7; 6|]
        let actual = quickSort pivotIndexSelectorFirstElement input
        let expected = 15
        Assert.Equal(expected, actual)
    
    let getExamInput () =
        let lines = File.ReadAllLines "quickSortExamData.txt" 
        lines |> Array.map (fun line -> System.Int32.Parse(line))

    [<Fact>]
    let `` examp input is correct`` () =
        let input = getExamInput()
        Assert.Equal(10000, input.Length)
    
    [<Fact>]
    let `` quickSort given examp input counts 162085 swaps and sorts input`` () =
        let input = getExamInput()
        let actual = quickSort pivotIndexSelectorFirstElement input
        let expected = 162085
        Assert.Equal(expected, actual)
        Assert.Equal(1, input.[0])
        Assert.Equal(2, input.[1])
        Assert.Equal(10000, input.[9999])
        Assert.Equal(9999, input.[9998])

    [<Fact>]
    let `` quickSort when given exam array with last element pivot selector returns 164123`` () =
        let input = getExamInput()
        let actual = quickSort pivotIndexSelectorLastElement input
        Assert.Equal(164123, actual)
    
    [<Fact>]
    let `` quickSort when given exam array with medial value pivot selector returns 138382`` () =
        let input = getExamInput()
        let actual = quickSort pivotIndexSelectorMedianElement input
        Assert.Equal(138382, actual)