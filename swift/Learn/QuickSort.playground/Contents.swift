var input: [Int] = [3, 8, 2, 5, 1, 4, 7, 6]

func pivot( inout array: [Int]) -> Void{
    let pivotElement = array[0]
    var unseenIndex = 1
    var comparisonSplitIndex = 0
    while unseenIndex < array.count{
        let unseenElement = array[unseenIndex]
        if unseenElement < pivotElement{
            swapArrayElements(&array, indexA: unseenIndex, indexB: comparisonSplitIndex + 1)
            comparisonSplitIndex += 1
        }
        unseenIndex += 1
    }
    swapArrayElements(&array, indexA: 0, indexB: comparisonSplitIndex)
}
func swapArrayElements(inout array: [Int], indexA: Int, indexB: Int) -> Void {
    let temp = array[indexA]
    array[indexA] = array[indexB]
    array[indexB] = temp
}

pivot(&input)