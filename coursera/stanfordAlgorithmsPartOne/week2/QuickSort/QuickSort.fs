namespace QuickSort

open PivotIndexSelector

module  QuickSorter = 
    
    let pivot (pivotIndexSelector: int array -> Range -> int) (input: int array) (rangeType: Range) =
        let range = createIndexRange rangeType input
        match range.Length with
        | 1 -> 
            0
        | rangeLength -> 
            let pivotIndex = pivotIndexSelector input rangeType
            let pivotItem = input.[pivotIndex]
            let mutable splitIndex = pivotIndex
            let mutable unseenIndex = splitIndex + 1
            while unseenIndex <= range.EndIndex do
                let unseenItem = input.[unseenIndex]
                if unseenItem < pivotItem then
                    splitIndex <- splitIndex + 1
                    swap input splitIndex unseenIndex |> ignore
                unseenIndex <- unseenIndex + 1
            swap input pivotIndex splitIndex |> ignore
            splitIndex

    let quickSort (pivotIndexSelector: int array -> Range -> int) (input: int array)=
        let mutable swapsCount = 0
        let rec quickSortRange (range: IndexRange) =
            let rangeLength = range.Length
            if rangeLength > 1 then
                swapsCount <- swapsCount + rangeLength - 1
                let pivotIndex = pivot pivotIndexSelector input (BetweenIndexes(range.StartIndex, range.EndIndex))
            
                if pivotIndex > range.StartIndex then
                    let leftRange = createIndexRange (BetweenIndexes(range.StartIndex, pivotIndex - 1)) input
                    quickSortRange leftRange
                if pivotIndex < range.EndIndex then
                    let rightRange = createIndexRange (BetweenIndexes(pivotIndex + 1, range.EndIndex)) input
                    quickSortRange rightRange
        quickSortRange (createIndexRange EntireArray input)
        swapsCount
                