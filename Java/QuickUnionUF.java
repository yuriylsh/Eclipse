public class QuickUnionUF
{
	// id[i] is parent of i
	private int[] id;	
	
	public QuickUnionUF(int N)
	{
		id = new int[N];
		for(int i = 0; i < N; i++)
		{
			id[i] = i;
		}
	}
	
	/*Root of i is id[id[id[...id[i]...]]]*/
	private int root(int i)
	{
		while(i != id[i])
		{
			i = id[i];
		}
		return i;
	}
	
	public boolean connected(int p, int q)
	{
		return root(p) = root(q);
	}
	
	// To merge components containing p and q
	// set the id of p's root to the id of q's root.
	public void union(int p, int q)
	{
		int i = root(p);
		int j = root(q);
		id[i] = j; 
	}
}