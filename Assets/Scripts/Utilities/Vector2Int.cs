
public struct Vector2Int
{
	public int x;
	public int y;
	
	public Vector2Int(int xs, int ys)
	{
		x = xs;
		y = ys;
	}
	
	public override string ToString()
	{
		return string.Format("[{0} - {1}]", x.ToString(), y.ToString());
	}	
}	