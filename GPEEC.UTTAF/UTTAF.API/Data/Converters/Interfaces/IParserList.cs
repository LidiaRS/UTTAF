using System.Collections.Generic;

namespace UTTAF.API.Data.Converters.Interfaces
{
	public interface IParserList<TOrigin, TDestiny>
	{
		IEnumerable<TDestiny> ParseList(IEnumerable<TOrigin> origin);
	}
}