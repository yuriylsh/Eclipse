namespace QuickSort

module PivotIndexSelector =

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

