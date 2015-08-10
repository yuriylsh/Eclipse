println("Hello world!")

var variable = 42
variable = 50
let constant = 33

let implicitInt = 70
let implicitDouble = 70.0
let explicitDouble: Double = 70
let explicitFloat: Float = 4

let label = "The width is "
let width = 94
let widthLabel = label + String(width)

let apples = 3
let oranges = 5
println("I have \(apples) apples.")
println("I have \(apples + oranges) pieces of fruit.")

var shoppingList = ["item 1", "item 2", "item 3"]
shoppingList[1] = "item for Dasha"

var occupations = [
    "Yuriy" : "husband",
    "Dasha" : "wife"
]
occupations["Andrey"] = "son"
occupations["Natasha"] = "daughter"
occupations

var emptyarray = [String]()
shoppingList = []
occupations = [:]

// control flow
let individualScores = [75, 43, 103, 87, 12]
var teamScore = 0
for score in individualScores{
    if score > 50 {
        teamScore += 3
    } else {
        teamScore += 1
    }
}
println(teamScore)
// optional and if with let
var optionaString: String? = "Hello"
println(optionaString == nil)
var optionalName: String? = "Yuriy"
var greeting = "Hello!"
if let name = optionalName{
    greeting = "Hello, \(name)"
}
greeting

let vegetable = "red pepper"
switch vegetable{
case "celer":
    let vegetableComment = "Add some raisins and make ants on a log."
case "cucamber", "watercress":
    let vegetableComment = "That would make a good tea sandwich."
case let x where x.hasSuffix("pepper"):
    let vegetableComment = "Is it a spicy \(x)?"
default:
    let vegetableComment = "Everything tastes good in soup."
}


let interestingNumbers = [
    "Prime": [2, 3, 5, 7, 11, 13],
    "Fibonacci": [1, 1, 2, 3, 5, 8],
    "Square": [1, 4, 9, 16, 25, 36]
]
var largest = 0
var largestType = ""
for (name, numbers) in interestingNumbers{
    for number in numbers{
        if number > largest{
            largest = number
            largestType = name
        }
    }
}
println("The largest number is \(largest) in \(largestType)")

var n = 2
while n < 100{
    n *= 2
}
var m = 2
do{ // in the new Swift do is renamed to repeat
    m *= 2
} while m < 100

var firstForLoop = 0
for i in 0..<4{ // or 0...3
    firstForLoop += i
}
var secondForLoop = 0
for var i = 0; i < 4; ++i{
    secondForLoop += i
}

func greet(name: String, day: String) -> String{
    return "Hello, \(name), today is \(day)"
}
greet("Yuriy", "Monday")

func calculateStatistics(scores: [Int]) -> (min: Int, max: Int, sum: Int){
    var min = scores[0]
    var max = scores[0]
    var sum = 0
    
    for score in scores{
        if min > score {
            min = score
        }
        if max < score {
            max = score
        }
        sum += score
    }
    return (min, max, sum)
}
let statistics = calculateStatistics([5, 3, 100, 3, 9])
print(statistics.sum)
print(statistics.2)

func sumOf(numbers: Int...) ->  Int {
    var sum = 0
    for number in numbers {
        sum += number
    }
    return sum
}
sumOf()
sumOf(42, 597, 12)

func returnFifteen() -> Int {
    var y = 10
    func add() { // nested function
        return y += 5
    }
    return y
}

func makeIncrementer() -> (Int -> Int) {
    func addOne(number: Int) -> Int { // functions are first-class type
        return 1 + number
    }
    return addOne;
}
let increment = makeIncrementer()
increment(7)

func hasAnyMatch (numbers: [Int], condition: Int -> Bool) -> Bool{
    for number in numbers{
        if condition(number) {
            return true
        }
    }
    return false
}
func lessThanTen (number: Int) -> Bool {
    return number < 10
}
var numbers = [20, 19, 7, 12]
hasAnyMatch(numbers, lessThanTen)

// closures
let mapped = numbers.map({
    (number: Int) -> Int in // use in to separate the args and return type from body
    let result = 3 * number
    return result
})
mapped
let mappedNumbers = numbers.map({number in 2 * number}) // type is known, so can ommit
mappedNumbers
numbers.sort({ $0 > $1 }) // can refer to parameters by number
numbers

//classes
class Shape{
    var numberOfSides = 0
    func simpleDescription () -> String  {
        return "A shape with \(numberOfSides) sides"
    }
}
var shape = Shape()
shape.numberOfSides = 87
shape.simpleDescription()

class NamedShape{
    var numberOfSides: Int = 0
    var name: String
    init(name: String) {
        self.name = name
    }

    func simpleDescription () -> String  {
        return "A shape with \(numberOfSides) sides"
    }
}
var namedShape = NamedShape(name: "oval")

class Square: NamedShape {
    var sideLength: Double
    init(sideLength: Double, name: String){
        self.sideLength = sideLength
        super.init(name: name)
        numberOfSides = 4
    }
    
    func area() -> Double {
        return sideLength * sideLength
    }
    
    override func simpleDescription() -> String {
        return "A square with sides of length \(sideLength)"
    }
}
let asquare = Square(sideLength: 4, name: "malevich")

class EquilateralTriangle: NamedShape {
    var sideLength: Double = 0
    init(sideLength: Double, name: String){
        self.sideLength = sideLength
        super.init(name: name)
        numberOfSides = 3
    }
    
    var perimeter: Double {
        get {
            return sideLength * 3.0
        }
        set {
            sideLength = newValue / 3.0
        }
            
    }
    override func simpleDescription() -> String {
        return "An equilateral triangle with sides of length \(sideLength)"
    }
}
var triangle = EquilateralTriangle(sideLength: 3.1, name: "a triangle")
triangle.perimeter
triangle.perimeter = 3.6

class TriangleAndSquare {
    var triangle:EquilateralTriangle{
        willSet{
            square.sideLength = newValue.sideLength
        }
    }
    var square: Square{
        willSet {
            triangle.sideLength = newValue.sideLength
        }
    }
    init(size: Double, name: String){
        square = Square(sideLength: size, name: name)
        triangle = EquilateralTriangle(sideLength: size, name: name)
    }
}
var triangleAndSquare = TriangleAndSquare(size: 10 ,name: "anohter test shape")
print(triangleAndSquare.square.sideLength)
print(triangleAndSquare.triangle.sideLength)
triangleAndSquare.square = Square(sideLength: 50, name: "larger square")
print(triangleAndSquare.triangle.sideLength)

let optionalSquare : Square? = Square(sideLength: 2.5, name: "optional square")
let sideLength = optionalSquare?.sideLength



enum Rank: Int {
    case Ace = 1
    case Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten
    case Jack, Queen, King
    func simpleDescription() -> String {
        switch self {
            case .Ace:
                return "Ace"
            case .Jack:
                return "Jack"
            case .Queen:
                return "Queen"
            case .King:
                return "King"
            default:
                return String(self.rawValue)
        }
    }
}
let ace = Rank.Ace
let aceRawValue = ace.rawValue
Rank.Queen.rawValue

if let convertedRank = Rank(rawValue: 11){
    print(convertedRank.simpleDescription())
}
enum Suit{
    case Hearts, Diamonds, Spades, Clubs
    func simpleDescription() -> String {
        switch self{
        case .Hearts:
            return "hearts"
        case .Diamonds:
            return "diamonds"
        case .Spades:
            return "spades"
        case .Clubs:
            return "clubs"
        }
    }
    func color() -> String{
        switch self{
        case .Spades, .Clubs:
            return "black"
        case .Diamonds, .Hearts:
            return "red"
        }
    }
}
let hearts = Suit.Hearts
print((hearts.simpleDescription(), hearts.color()))

struct Card{ // always copied when passed around
    var rank: Rank
    var suit: Suit
    init(rank: Rank, suit: Suit){
        self.rank = rank
        self.suit = suit
    }
    func simpleDescription() -> String{
        return "The \(rank.simpleDescription()) of \(suit.simpleDescription())"
    }
    func createFullDeck() -> [Card]{
        var deck = [Card]()
        for rankRaw in 1...13{
            let currentRank = Rank(rawValue: rankRaw)!
            deck.append(Card(rank: currentRank, suit: .Hearts))
            deck.append(Card(rank: currentRank, suit: .Diamonds))
            deck.append(Card(rank: currentRank, suit: .Spades))
            deck.append(Card(rank: currentRank, suit: .Clubs))

        }
        return deck
    }
}

var card = Card(rank: .Seven, suit: .Spades)
print(card.simpleDescription())
let deck = card.createFullDeck().map({$0.simpleDescription()})
deck

// enum with associated values (differs from raw values)
enum ServerResponse{
    case Result(String, String)
    case Error(String)
}
let success = ServerResponse.Result("6:09AM", "8:05PM")
let failure = ServerResponse.Error("Out of cheese")
var serverResponse: String
switch success{
case let .Result(sunrise, sunset):
    serverResponse = "The sunrise is at \(sunrise), the sunset is at \(sunset)"
case let .Error(error):
    serverResponse = "Failure... \(error)"
}
serverResponse

// protocols and extensions
protocol SimpleProtocol{ // can be implemented by classes, structs and enums
    var simpleDescription: String { get }
    mutating func adjust()
}
class SimpleClass : SimpleProtocol{
    var simpleDescription: String = "A simple class."
    var someOtherProperty: Int = 1983
    func adjust() { // no need for mutating keyword because class methods are always mutating
        simpleDescription += " Now 100% adjusted."
    }
}
let simpleClass = SimpleClass()
simpleClass.adjust()
let abc: SimpleProtocol = simpleClass
abc.simpleDescription

extension Int: SimpleProtocol{
    var simpleDescription: String { return "the number \(self)"}
    mutating func adjust() {
        self += 42
    }
}
print(7.simpleDescription)






