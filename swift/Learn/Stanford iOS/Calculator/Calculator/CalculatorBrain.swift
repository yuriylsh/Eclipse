//
//  CalculatorBrain.swift
//  Calculator
//
//  Created by imodules on 8/26/15.
//  Copyright (c) 2015 imodules. All rights reserved.
//

import Foundation

class CalculatorBrain{
    private enum Op{
        case Operand(Double)
        case UnaryOperation(String, Double -> Double)
        case BinaryOperation(String, (Double, Double) -> Double)
    }
    
    private var opStack = [Op]()
    private var knownOperations = [String:Op]()
    
    init(){
        knownOperations["✕"] = Op.BinaryOperation("✕", *)
        knownOperations["÷"] = Op.BinaryOperation("÷") {$1 / $0}
        knownOperations["+"] = Op.BinaryOperation("+", +)
        knownOperations["-"] = Op.BinaryOperation("-") {$1 - $0}
        knownOperations["√"] = Op.UnaryOperation("√", sqrt)
    }
    
    private func evaluate(ops: [Op]) -> (result: Double?, remainingOps: [Op]){
        return (3, [])
    }
    
    func evaluate () -> Double?{
        return 3
    }
    
    func pushOperand(operand: Double){
        opStack.append(Op.Operand(operand))
    }
    
    func performOperand(symbol: String){
        if let operation = knownOperations[symbol]{
            opStack.append(operation)
        }
    }
}