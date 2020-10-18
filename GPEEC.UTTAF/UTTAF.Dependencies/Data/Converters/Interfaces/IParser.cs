﻿namespace UTTAF.Dependencies.Data.Converters.Interfaces
{
	public interface IParser<TOrigin, TDestiny>
	{
		TDestiny Parse(TOrigin origin);
	}
}