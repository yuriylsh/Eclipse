package com.dayngo.pluralsight.javafundamentals.thejavalanguage.initialization;

import java.util.Arrays;

public class FlightInitializationBlocks {
    private int passengers, flightNumber, seats = 150;
    private char flightClass;
    private boolean[] isSeatAvailable;

    {
        isSeatAvailable = new boolean[seats];
        for (int i = 0; i < seats; i++) isSeatAvailable[i] = true;
        System.out.println(Arrays.toString(isSeatAvailable));
    }

    public FlightInitializationBlocks() {}

    public FlightInitializationBlocks(int flightNumber) {
        this.flightNumber = flightNumber;
    }

    public FlightInitializationBlocks(char flightClass) {
        this.flightClass = flightClass;
    }
}
