using System.Collections.Generic;

namespace UTTAF.Dependencies.Data.Converters.Interfaces
{
	public interface IParserList<TOrigin, TDestiny>
	{
		IEnumerable<TDestiny> ParseList(IEnumerable<TOrigin> origin);
	}
}