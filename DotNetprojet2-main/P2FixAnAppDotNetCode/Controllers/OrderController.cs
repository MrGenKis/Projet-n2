using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICart _cart;
        private readonly IOrderService _orderService;
        private readonly IStringLocalizer<OrderController> _localizer;

        public OrderController(ICart pCart, IOrderService service, IStringLocalizer<OrderController> localizer)
        {
            _cart = pCart;
            _orderService = service;
            _localizer = localizer;
        }

        public ViewResult Index() => View(new Order());

        [HttpPost]
        public IActionResult Index(Order order)
        {
            Debug.WriteLine("🚀 Requête POST reçue !");
            Debug.WriteLine($"📌 ModelState.IsValid : {ModelState.IsValid}");

            // 🛠️ Étape 1 : Vérifier si le panier est vide  
            Cart cart = _cart as Cart;
            if (cart == null || !cart.Lines.Any())
            {
                Debug.WriteLine("❌ Le panier est vide");
                ModelState.AddModelError("", _localizer["CartEmpty"]);
                return View(order);
            }

            // 🛠️ Étape 2 : Vérifier que tous les champs obligatoires sont remplis  
            if (string.IsNullOrWhiteSpace(order.Name))
            {
                Debug.WriteLine("❌ Le champ Name est vide");
                ModelState.AddModelError("Name", _localizer["ErrorMissingName"]);
            }

            if (string.IsNullOrWhiteSpace(order.Address))
            {
                Debug.WriteLine("❌ Le champ Address est vide");
                ModelState.AddModelError("Address", _localizer["ErrorMissingAddress"]);
            }

            if (string.IsNullOrWhiteSpace(order.City))
            {
                Debug.WriteLine("❌ Le champ City est vide");
                ModelState.AddModelError("City", _localizer["ErrorMissingCity"]);
            }

            if (string.IsNullOrWhiteSpace(order.Country))
            {
                Debug.WriteLine("❌ Le champ Country est vide");
                ModelState.AddModelError("Country", _localizer["ErrorMissingCountry"]);
            }

            // 🛠️ Étape 3 : Vérifier s'il y a des erreurs avant de continuer  
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("❌ Formulaire invalide, retour à la page !");
                return View(order);
            }

            // 🛠️ Étape 4 : Ajouter les articles du panier à la commande  
            order.Lines = cart.Lines.ToArray();

            // 🛠️ Étape 5 : Enregistrer la commande  
            _orderService.SaveOrder(order);

            // 🛠️ Étape 6 : Rediriger vers la page de confirmation  
            return RedirectToAction(nameof(Completed));
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}
