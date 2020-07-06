using System.Threading.Tasks;

namespace UTTAF.Dependencies.Clients.Interfaces
{
	public interface IConnectionManager
	{
		Task ConnectAsync();

		Task DesconeectAsync();
	}
}