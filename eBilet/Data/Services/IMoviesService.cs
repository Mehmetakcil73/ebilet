using eBilet.Data.Base;
using eBilet.Data.ViewModels;
using eBilet.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eBilet.Data.Services
{
	public interface IMoviesService: IEntityBaseRepository<Movie>
	{
		Task<Movie> GetMovieByIdAsync(int id);
		Task<NewMovieDropdownsVM> GetNewMovieDropdownValues();
		Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
    }
}
