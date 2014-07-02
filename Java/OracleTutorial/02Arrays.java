class Arrays{
	public static void main(String[] args) {
		int[] intArray;
		intArray = new int[3];
		System.out.println("intArray[0]: " + intArray[0]);
		System.out.println("intArray[1]: " + intArray[1]);
		System.out.println("intArray[2]: " + intArray[2]);

		int[]  intArray2 = {100, 200, 300, 400, 500};
		System.out.println("intArray2[0]: " + intArray2[0]);
		System.out.println("intArray2[1]: " + intArray2[1]);
		System.out.println("intArray2[2]: " + intArray2[2]);

		String[][] names = {
			{"Mr. ", "Mrs. ", "Ms. "},
			{"Lyeshchenko", "Mikhno"}
		};
		// Mr. Lyeshchenko
		System.out.println(names[0][0] + names[1][0]);
		// Ms. Mikhno
		System.out.println(names[0][2] + names[1][1]);

		char[] copyFrom = { 'd', 'e', 'c', 'a', 'f', 'f', 'e', 'i', 'n', 'a', 't', 'e', 'd' };
        char[] copyTo = new char[7];

        System.arraycopy(copyFrom, /*srcPos*/2, copyTo, /*destPos*/0, /*length*/7);
        System.out.println(new String(copyTo));
        System.out.println("copyTo.lenght: " + copyTo.length);

        // java.util.Arrays has a lot of methods to manipulate arrays
        char[] copyTo2 = java.util.Arrays.copyOfRange(copyFrom, 2, 9);
        System.out.println(new String(copyTo2));
	}
}