class Custom{
	public static void main(String[] args){
		Derived d = new Derived();
		d.baseMethod1();
		d.baseMethod2();
		d.baseMethod3();

		System.out.println();

		Derived2 d2 = new Derived2();
		d2.baseMethod1();
		d2.baseMethod2();
		d.baseMethod3();
	}
}

class Base1{
	public void baseMethod1(){
		System.out.println("Base1.baseMethod1");
	}
}

interface IBase2{
	void baseMethod2();
}

interface IBase3{
	void baseMethod3();
}

class Derived extends Base1 implements IBase2, IBase3{
	public void baseMethod2(){
		System.out.println("IBase2.baseMethod2 implementation");
	}
	public void baseMethod3(){
		System.out.println("IBase3.baseMethod3 implementation");
	}
}

class SomeOtherBase{
	public void baseMethod1(){
		System.out.println("Base1.baseMethod1");
	}
	public void baseMethod2(){
		System.out.println("IBase2.baseMethod2 implementation");
	}
	public void baseMethod3(){
		System.out.println("IBase3.baseMethod3 implementation");
	}
}

class Derived2 extends SomeOtherBase{
}
