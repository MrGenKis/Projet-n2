using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;
using Microsoft.Extensions.Logging; 

namespace P2FixAnAppDotNetCode.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cart;
        private readonly IProductService _productService;
        private readonly ILogger<CartController> _logger; 

        public CartController(ICart pCart, IProductService productService, ILogger<CartController> logger) //constructer de la class (le constructeur = methode qui a le meme nom de la classe et qui ne retourne aucune valeur)
        {
            _cart = pCart;
            _productService = productService;
            _logger = logger; 
        }

        public ViewResult Index() //methode qui appelle la view index en get(get = affichage)
        {
            return View(_cart as Cart);
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(int id) //resultat du clic sur un boutton qui va declacher quelque chose.
        {
            _logger.LogInformation($"AddToCart() a été appelée avec l'ID {id} !");

            Product product = _productService.GetProductById(id);

            if (product != null)
            {
                _logger.LogInformation($"Produit trouvé : {product.Name}");
                _cart.AddItem(product, 1);
            }
            else
            {
                _logger.LogWarning("Produit introuvable !");
            }

            return RedirectToAction("Index");
        }


        public RedirectToActionResult RemoveFromCart(int id)
        {
            Product product = _productService.GetAllProducts()
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                _cart.RemoveLine(product);
            }
            return RedirectToAction("Index");
        }
    }
}
