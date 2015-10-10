//
//  ViewController.swift
//  Calculator
//
//  Created by imodules on 8/22/15.
//  Copyright (c) 2015 imodules. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    
    @IBOutlet weak var display: UILabel!
    var userIsInTheMiddleOfTypingNumber = false
    
    @IBAction func appendDigit(sender: UIButton) {
        let digit = sender.currentTitle!
        if userIsInTheMiddleOfTypingNumber{
            display.text = display.text! + digit
        }else{
            display.text = digit
            userIsInTheMiddleOfTypingNumber = true
        }
    }
    
    @IBAction func operate(sender: UIButton) {
        let operand = sender.currentTitle!
        if userIsInTheMiddleOfTypingNumber{
            enter()
        }
        switch operand{
        case "✕":
            performOperationOnTwoOperands { $0 * $1 }
        case "÷":
            performOperationOnTwoOperands { $1 / $0 }
        case "+":
            performOperationOnTwoOperands { $0 + $1 }
        case "-":
            performOperationOnTwoOperands { $1 - $0 }
        case "√":
            performOperationOnSingleOperand { sqrt ($0) }
        default: break
        }
    }
    
    func performOperationOnTwoOperands(operation: (Double, Double) -> Double){
        if operandStack.count >= 2 {
            displayValue = operation(operandStack.removeLast(), operandStack.removeLast())
            enter()
        }
    }
    
    func performOperationOnSingleOperand(operation: Double -> Double){
        if operandStack.count >= 1 {
            displayValue = operation(operandStack.removeLast())
            enter()
        }
    }
    
    var operandStack: Array<Double> = Array<Double>()
    @IBAction func enter() {
        userIsInTheMiddleOfTypingNumber = false
        operandStack.append(displayValue)
        println(operandStack)
    }
    
    var displayValue: Double {
        get{
            return NSNumberFormatter().numberFromString(display.text!)!.doubleValue
        }
        set{
            display.text = "\(newValue)"
            userIsInTheMiddleOfTypingNumber = false
        }
    }
}

