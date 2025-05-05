using eBilet.Data.Base;
using eBilet.Models;

namespace eBilet.Data.Services
{
	public class ProducersServices: EntityBaseRepository<Producer>, IProducersService
	{
        public ProducersServices(AppDbContext context) : base(context)
        {
            
        }
    }
}
