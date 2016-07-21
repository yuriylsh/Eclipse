print("Hello, world")
let explicitFloat: Float = 4
let label = "The width is "
let convertedToString = label + String(explicitFloat)
let stringInterpolation = "I have \(explicitFloat) apples."

var nonEmptyArray = [1, 2, 3]
nonEmptyArray[0] = 100
let emptyArray = [String]()
var nonEmptyDictionary: [Int:String?] = [
    1: "Yuriy",
    2: "Dasha",
    3: "Andrey",
    4: "Natasha"
]
nonEmptyDictionary[4] = "Natashka"
print(nonEmptyDictionary)

let emptyDictionary = [Int: String]()

for (id, name) in nonEmptyDictionary{
    if let person = name {
        print("Hello \(person)")
    } else {
        print("Hello, Unknown Person")
    }
}

let nickName: String? = nil
let name = "Yuriy"
print("Hello, \(nickName ?? name).")

let vegetable = "red pepper"
switch vegetable {
case "tomato":
    print("I like tomatoes")
case "cucumber", "pickle":
    print("I like those too")
case let x where x.hasSuffix("pepper"):
    print("I hope \(x) is not very hot.")
default:
    print("I hope it's good")
}

let interestingNumbers = [
    "Prime": [2, 3, 5, 7, 11, 13],
    "Fibonacci": [1, 1, 2, 3, 5, 8],
    "Square": [1, 4, 9, 16, 25],
]
var largest = 0
for (type, numbers) in interestingNumbers {
    for number in numbers {
        if number > largest {
            largest = number
        }
    }
}
print("The largest number is \(largest)")

for number in 1..<3 {
    print("number is: \(number)")
}
for number in 1...3 {
    print("number is \(number)")
}

func greet(person: String, day: String) -> String{
    return "Hello \(person)! Today is \(day)"
}
print(greet("Yuriy", day: "Tuesday"))
func greet_withLabels(person: String, on day: String) -> String {
    return "Hello \(person)! Today is \(day)"
}
print(greet_withLabels("Yuriy", on: "Wednesday"))
func calculateStatistics(numbers: [Int]) -> (min: Int, max: Int, sum: Int){
    var min = numbers[0],
        max = numbers[0],
        sum = 0
    for number in numbers {
        if number < min {
            min = number
        } else if number > max {
            max = number
        }
        sum += number
    }
    return (min, max, sum)
}
let statistics = calculateStatistics([5, 3, 100, 3, 9])
print("min = \(statistics.min), max = \(statistics.1), sum = \(statistics.sum)")

