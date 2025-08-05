using CosmeticsStoreDB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsStoreDB.Controllers
{
    public class CartController : Controller
    {
        private readonly CartManager _cartmanager;
        private readonly CosmeticsManager _cosmeticsmanager;

        public CartController(CartManager cartmanager, CosmeticsManager cosmeticsmanager)
        {
            _cartmanager = cartmanager;
            _cosmeticsmanager = cosmeticsmanager;
        }

        public IActionResult Index()
        {
            ViewBag.Total = _cartmanager.Total();
            return View(_cartmanager.Items);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int qty = 1)
        {
            var p = _cosmeticsmanager.Get(productId);
            if (p != null && qty > 0 && qty <= p.AvailableQuantity)
                _cartmanager.AddToCart(p, qty);

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            _cartmanager.RemoveFromCart(productId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            ViewBag.Total = _cartmanager.Total();
            return View(_cartmanager.Items);
        }

        [HttpPost]
        public IActionResult CheckoutConfirmed()
        {
            foreach (var item in _cartmanager.Items)
            {
                var p = _cosmeticsmanager.Get(item.ProductID);
                if (p != null)
                {
                    p.AvailableQuantity -= item.Quantity;
                    _cosmeticsmanager.Update(p);
                }
            }
            _cartmanager.Clear();
            return RedirectToAction(nameof(Index), "Product");
        }
    }
}
