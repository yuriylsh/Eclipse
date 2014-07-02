class Operators{
	public static void main(String[] args){
		System.out.println("3 % 7 = " + (3 % 7));
		System.out.println("13 % 7 = " + (13 % 7));
		System.out.println("8 / 7 = " + (8 / 7));
		int num = 3;
		num += 1;
		System.out.println("3+=1 = " + num);

		// the Type Comparison Operator instanceof
		Parent parent = new Parent();
		Parent child  = new Child();
		System.out.println("parent instanceof Parent: " + 
				(parent instanceof Parent));
		System.out.println("parent instanceof Child: " + 
				(parent instanceof Child));
		System.out.println("parent instanceof MyInterface: " + 
				(parent instanceof MyInterface));
		System.out.println("child instanceof Parent: " + 
				(child instanceof Parent));
		System.out.println("child instanceof Child: " + 
				(child instanceof Child));
		System.out.println("child instanceof MyInterface: " + 
				(child instanceof MyInterface));
	}
}

interface MyInterface{}
class Parent{}
class Child extends Parent implements MyInterface{}
