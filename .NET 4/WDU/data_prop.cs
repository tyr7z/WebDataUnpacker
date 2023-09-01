namespace WDU;

internal class data_prop
{
	public uint ofs;

	public uint size;

	public string name;

	public data_prop(uint _ofs, uint _size, string _name)
	{
		ofs = _ofs;
		size = _size;
		name = _name;
	}
}
