using System.Linq;
using System.Threading.Tasks;
using eBilet.Data;
using eBilet.Data.Services;
using eBilet.Models;
using Microsoft.AspNetCore.Mvc;

namespace eBilet.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAll();
            return View(data);
        }


        //Get Actors/Create
        public IActionResult Create()
        {
            return View();
        }
    }
} 
