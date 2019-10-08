package com.dayngo.pluralsight.javafundamentals.thejavalanguage.initialization;

import java.util.Arrays;

public class FlightConstructors {
    private int passengers, flightNumber, seats = 150;
    private char flightClass;
    private boolean[] isSeatAvailable;

    public FlightConstructors() {
        isSeatAvailable = new boolean[seats];
        for (int i = 0; i < seats; i++) isSeatAvailable[i] = true;
        System.out.println(Arrays.toString(isSeatAvailable));
    }

    public FlightConstructors(int flightNumber) {
        this();
        this.flightNumber = flightNumber;
    }

    public FlightConstructors(char flightClass) {
        this();
        this.flightClass = flightClass;
    }
}
