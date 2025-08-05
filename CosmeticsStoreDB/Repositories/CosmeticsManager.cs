using CosmeticsStoreDB.Data;
using CosmeticsStoreDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsStoreDB.Repositories
{
    public class CosmeticsManager
    {
        private CosmeticsContext _context;
        public CosmeticsManager(CosmeticsContext context)
        {
            _context = context;
        }
        public CosmeticsManager AddProduct(ProductModel productModel)
        {
            _context.Products.Add(productModel);
            _context.SaveChanges();

            return this;
        }

        public List<ProductModel> GetProducts()
        {
            var cosmetics = _context.Products.ToList();
            return cosmetics;
        }

        public List<ProductModel> GetAll() => _context.Products.ToList();

        public ProductModel Get(int id) => _context.Products.Find(id);

        public void Add(ProductModel p)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
        }

        public void Update(ProductModel p)
        {
            _context.Products.Update(p);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var p = Get(id);
            if (p != null)
            {
                _context.Products.Remove(p);
                _context.SaveChanges();
            }
        }

        public bool TryTakeProduct(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null || product.AvailableQuantity < quantity)
                return false; // Not enough stock

            product.AvailableQuantity -= quantity;
            _context.SaveChanges();
            return true;
        }

        public bool TryAddToCartAndDecreaseStock(int productId, int quantityToAdd)
        {
            var product = _context.Products.Find(productId);
            if (product == null || product.AvailableQuantity < quantityToAdd)
                return false; // Not enough stock

            product.AvailableQuantity -= quantityToAdd;
            _context.SaveChanges();

            // Add to cart logic here (e.g., update cart table or session)
            return true;
        }

        public bool TryAddToCartWithStockCheck(int productId, int quantityToAdd, int cartQuantityAlready)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
                return false;

            // Check if enough stock remains for the total cart quantity
            if (product.AvailableQuantity < quantityToAdd)
                return false;

            // Optionally: Check if (cartQuantityAlready + quantityToAdd) <= (product.AvailableQuantity + cartQuantityAlready)
            // This ensures you don't exceed the original stock

            product.AvailableQuantity -= quantityToAdd;
            _context.SaveChanges();

            // Add to cart logic here
            return true;
        }

        public IActionResult OnPostAddToCart(int productId, int quantity)
        {
            if (TryAddToCartAndDecreaseStock(productId, quantity))
            {
                // Add to cart logic (session, DB, etc.)
                return new RedirectToPageResult("Cart");
            }
            else
            {
                // ModelState.AddModelError("", "Not enough stock available.");
                // return Page();
                return new BadRequestResult();
            }
        }
    }
}

