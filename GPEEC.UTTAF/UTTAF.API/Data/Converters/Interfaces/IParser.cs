using System.Collections.Generic;

namespace UTTAF.API.Data.Converters.Interfaces
{
	public interface IParser<TOrigin, TDestiny>
	{
		TDestiny Parse(TOrigin origin);

		List<TDestiny> ParseList(List<TOrigin> origin);
	}
}