﻿using eBilet.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eBilet.Data.Services;
using eBilet.Models;
using Microsoft.AspNetCore.Authorization;

namespace eBilet.Controllers
{
    [Authorize]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

		[AllowAnonymous]
		public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }

        //GET: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

		//GET Cinemas/Details/1
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

		//GET: Cİnemas/Edit/1

		public async Task<IActionResult> Edit(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}
		[HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

		//GET: Cİnemas/Delete/1

		public async Task<IActionResult> Delete(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirm(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}  
}
