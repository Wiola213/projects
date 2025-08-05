using CosmeticsStoreDB.Models;

namespace CosmeticsStoreDB.Repositories
{
    public class CartManager
    {
        private readonly List<CartProductModel> _items = new();

        public IReadOnlyList<CartProductModel> Items => _items;

        public void AddToCart(ProductModel p, int qty)
        {
            var existing = _items.FirstOrDefault(x => x.ProductID == p.ID);
            if (existing == null)
            {
                _items.Add(new CartProductModel
                {
                    ProductID = p.ID,
                    ProductName = p.Name,
                    UnitPrice = p.Price,
                    Quantity = qty
                });
            }
            else
            {
                existing.Quantity += qty;
            }
        }

        public void RemoveFromCart(int productId)
        {
            var existing = _items.FirstOrDefault(x => x.ProductID == productId);
            if (existing != null) _items.Remove(existing);
        }

        public decimal Total() =>
            _items.Sum(x => x.UnitPrice * x.Quantity);

        public void Clear() => _items.Clear();
    }
}

