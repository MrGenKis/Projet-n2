using System;
using P2FixAnAppDotNetCode.Models.Repositories;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// Provides services to manage an order
    /// </summary>
    public class OrderService : IOrderService
    {
       private readonly ICart _cart;
       private readonly IOrderRepository _repository;
       private readonly IProductService _productService;

        public OrderService(ICart cart, IOrderRepository orderRepo, IProductService productService)
        {
            _repository = orderRepo;
            _cart = cart;
            _productService = productService;
        }

        /// <summary>
        /// Saves an order
        /// </summary>
        public void SaveOrder(Order order)
        {
            order.Date = DateTime.Now;
            _repository.Save(order);
            UpdateInventory();
        }

        /// <summary>
        /// Update the product quantities inventory and clears the cart
        /// </summary>
        private void UpdateInventory()
        {
            // 🛠️ Étape 1 : Vérifie que `_productService.UpdateProductQuantities()` est bien appelée.
            // ➡️ Ajoute un message pour vérifier que cette méthode est exécutée.
            Console.WriteLine("UpdateInventory est appelée");

            _productService.UpdateProductQuantities(_cart as Cart);

            // 🛠️ Étape 2 : Vérifie que `_cart.Clear()` n'efface pas le panier avant que les quantités ne soient mises à jour.
            // ➡️ Si le panier est vidé trop tôt, les quantités seront toujours nulles au moment de l’update.
            Console.WriteLine(" Panier vidé après mise à jour du stock");
            _cart.Clear();
        }

    }
}
