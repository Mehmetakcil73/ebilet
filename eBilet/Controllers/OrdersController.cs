﻿using eBilet.Data.Cart;
using eBilet.Data.Services;
using eBilet.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eBilet.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		private readonly IMoviesService _moviesService;
		private readonly ShoppingCart _shoppingCart;
		private readonly IOrdersService _ordersService;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
			_shoppingCart = shoppingCart;
			_ordersService = ordersService;
        }
        public IActionResult ShoppingCart()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;

			var response = new ShoppingCartVM()
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCarTotal()
			};

			return View(response);
		}
		
		public async Task<IActionResult> AddItemToShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);

			if(item !=null)
			{
				_shoppingCart.AddItemCart(item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}
		public async Task<IActionResult> Index()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string userRole = User.FindFirstValue(ClaimTypes.Role);
			var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
			return View(orders);
		}

		public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);

			if(item != null)
			{
				_shoppingCart.RemoveItemFromCart(item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}
		public async Task<IActionResult> CompleteOrders()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

			await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
			await _shoppingCart.ClearShoppingCartAsync();
			return View("OrderCompleted");
		}

    }
}
