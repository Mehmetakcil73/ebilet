using eBilet.Data.Base;
using eBilet.Models;

namespace eBilet.Data.Services
{
	public class CinemasService: EntityBaseRepository<Cinema>, ICinemasService
	{
        public CinemasService(AppDbContext context) : base(context) 
        {
            
        }
    }
}
