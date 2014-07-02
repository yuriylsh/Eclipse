package com.yuriylsh.algs4partI;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class UnionFindRunner {
	public static void main(String args[])
	{
		int N = ReadInt();
		System.out.println(N);
		
	}
	
	private static BufferedReader bufferReader = new BufferedReader(new InputStreamReader(System.in));
	public static BufferedReader getBufferReader() {
		return bufferReader;
	}
	public static void setBufferReader(BufferedReader bufferReader) {
		UnionFindRunner.bufferReader = bufferReader;
	}
	
	public static String ReadString()
	{
		String input = "";
		try{
			input = bufferReader.readLine();
		} catch(IOException ex)
		{
			ex.printStackTrace();
		}
		return input;
	}
	
	public static int ReadInt()
	{
		String intString = ReadString();
		int result = Integer.MIN_VALUE;
		try{
			result = Integer.parseInt(intString);
		} catch(NumberFormatException ex)
		{
			ex.printStackTrace();
		}
		return result;
	}
	
}
