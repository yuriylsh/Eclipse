namespace QuickSort.Tests
open QuickSort.QuickSorter
open Xunit
open System.IO

module QuickSorterTests =

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
    let `` quickSort when given exam array with last element pivot selector returns 164123`` () =
        let input = getExamInput()
        let actual = quickSort pivotIndexSelectorLastElement input
        Assert.Equal(164123, actual)

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

    
    [<Fact>]
    let `` quickSort when given exam array with medial value pivot selector returns 138382`` () =
        let input = getExamInput()
        let actual = quickSort pivotIndexSelectorMedianElement input
        Assert.Equal(138382, actual)