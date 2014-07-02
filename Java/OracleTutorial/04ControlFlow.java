class ControlFlow{
	public static void main(String[] args){
		System.out.println("Control flow");
		Switch("Yuriy");
		Switch("Dasha");
		Switch("Andrey");
		
		// C# foreach replacement
		int[] intArray = {1, 2, 3, 4, 5};
		for(int number : intArray){
			System.out.print(number + "\t");
		}
		System.out.println("");
		
		// labeled break. Does not exists in C#
		int[][] arrayOfInts = {
			{32, 87, 3, 589},
			{12, 1076, 2000, 8},
			{622, 127, 77, 955}
		};	
		int searchFor = 12, i = 0, j = 0;
		boolean foundIt = false;
search:
		for(i = 0; i < arrayOfInts.length; i++){
			for(j = 0; j < arrayOfInts[i].length; j++){
				if(arrayOfInts[i][j] == searchFor){
					foundIt = true;
					break search;
				}
			}
		}
		if(foundIt){
			System.out.println("Found" + searchFor + 
					" at arrayOfInts[" + i + "][" + j + "]");
		}
	}
	
	public static void Switch(String str){
		String name;
		switch (str) {
			case "Yuriy":
			case "Dasha":
				name = "Lyeshchenko";
				break;
			default:
				name = "unkonwn";
				break;
		}
		System.out.println("Result of switch over " + str + " is: " + name);
	}
}
