using UTTAF.API.Data;

namespace UTTAF.API.Repository
{
    public class Repository
    {
        protected readonly DataContext _context;

        protected Repository(DataContext context) => _context = context;
    }
}