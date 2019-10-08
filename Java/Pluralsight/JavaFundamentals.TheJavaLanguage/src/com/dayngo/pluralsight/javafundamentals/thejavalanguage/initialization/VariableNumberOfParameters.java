package com.dayngo.pluralsight.javafundamentals.thejavalanguage.initialization;

public class VariableNumberOfParameters {
    public void IAcceptVariableNumberOfParameters(int... numbers) {
        for (int number : numbers) {
            System.out.println(number);
        }
    }
}
