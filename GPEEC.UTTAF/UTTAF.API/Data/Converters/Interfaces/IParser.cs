namespace UTTAF.API.Data.Converters.Interfaces
{
	public interface IParser<TOrigin, TDestiny>
	{
		TDestiny Parse(TOrigin origin);
	}
}