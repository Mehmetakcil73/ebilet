using eBilet.Data.Base;
using eBilet.Models;

namespace eBilet.Data.Services
{
	public class ProducersService: EntityBaseRepository<Producer>, IProducersService
	{
        public ProducersService(AppDbContext context) : base(context)
        {
            
        }
    }
}
