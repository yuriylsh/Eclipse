package com.helloworld.yuriy;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public final class First {
	public static void main(String args[])
	{
		writeHello();
	}
	
	public static void writeHello(){
		System.out.print("Your name: ");
		String input = "";
		BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
		try {
			input = reader.readLine();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		System.out.println("Hello " + input + "!");
	}
}
