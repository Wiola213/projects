using CosmeticsStoreDB.Models;
using CosmeticsStoreDB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsStoreDB.Controllers
{
    public class ProductController : Controller
    {
        private CosmeticsManager _manager;
        public ProductController(CosmeticsManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            var film = _manager.GetProducts();

            return View(film);
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(ProductModel p)
        {
            if (!ModelState.IsValid) return View(p);
            _manager.Add(p);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var p = _manager.Get(id);
            return p == null ? NotFound() : View(p);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel p)
        {
            if (!ModelState.IsValid) return View(p);
            _manager.Update(p);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var p = _manager.Get(id);
            return p == null ? NotFound() : View(p);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _manager.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public IActionResult AddToCart(int id, int quantity)
        //{
        //    // Przykład użycia managera do dodania produktu do koszyka
        //    // Możesz użyć np. CosmeticsManager.TryAddToCartAndDecreaseStock lub innej metody
        //    bool success = _manager.TryAddToCartAndDecreaseStock(id, quantity);
        //    if (!success)
        //    {
        //        // Obsłuż błąd, np. brak dostępnej ilości
        //        TempData["Error"] = "Nie można dodać produktu do koszyka.";
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
