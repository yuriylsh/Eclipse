namespace QuickSort

module  QuickSorter = 
    
    type Range =
    | EntireArray
    | BetweenIndexes of int * int
    
    type IndexRange = {StartIndex: int; EndIndex: int} with 
        member this.Length =
            this.EndIndex - this.StartIndex + 1

    let createIndexRange (range:Range) (input: int array) =
        match range with
        | EntireArray -> {StartIndex = 0; EndIndex = (input.Length - 1)}
        | BetweenIndexes (startIndex, endIndex) -> {StartIndex = startIndex; EndIndex = endIndex}
    
    let pivotIndexSelectorFirstElement (input: int array) (range: Range) =
        (createIndexRange range input).StartIndex
    
    let swap (input: int array) (firstIndex: int) (secondIndex: int) =
        let temp = input.[firstIndex]
        input.[firstIndex] <- input.[secondIndex]
        input.[secondIndex] <- temp

    let pivotIndexSelectorLastElement (input: int array) (range: Range) =
        let indexRange = createIndexRange range input
        if indexRange.Length > 1 then
            swap input (indexRange.StartIndex) (indexRange.EndIndex) |> ignore
        indexRange.StartIndex

    let selectMedianValuePivotIndex (input: int array) (indexes: int list) =
        let orderedValues = indexes 
                                |> List.map (fun index -> (index, input.[index]))
                                |> List.sortBy((fun (index, value) -> value))
                        
        fst orderedValues.[1]

    let swapMedianValueIndexAndStartIndex (input:int array) (indexRange: IndexRange) (middleOffset: int) =
        let medianValueIndex = selectMedianValuePivotIndex input ([indexRange.StartIndex; indexRange.StartIndex + middleOffset; indexRange.EndIndex])
        swap input (indexRange.StartIndex) medianValueIndex
        indexRange.StartIndex

    let pivotIndexSelectorMedianElement (input:int array) (range: Range) =
        let indexRange = createIndexRange range input
        match indexRange.Length with
        | 0 | 1 -> indexRange.StartIndex
        | oddLength when oddLength % 2 = 1 ->
            let middleOffset = oddLength / 2
            swapMedianValueIndexAndStartIndex input indexRange middleOffset
        | evenLength ->
            let middleOffset = evenLength / 2 - 1
            swapMedianValueIndexAndStartIndex input indexRange middleOffset

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
                