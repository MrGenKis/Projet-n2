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
            // 🛠️ Étape 1 : Vérifier si le panier est vide  
            // ➡️ `_cart` est de type `ICart`, il faut donc le caster en `Cart` pour accéder aux lignes.  
            // ➡️ Vérifie que `cart.Lines` contient des éléments en utilisant `.Any()`.  
            // ➡️ Si le panier est vide, afficher un message d'erreur et renvoyer une vue avec une erreur.

            Cart cart = _cart as Cart; // 🛠️ Convertir `_cart` en `Cart`

            if (cart == null || !cart.Lines.Any())
            {
                Debug.WriteLine("❌ Le panier est vide");
                ModelState.AddModelError("", _localizer["CartEmpty"]); // 🛠️ Ajouter un message d'erreur localisé  
                return View(order); // 🛠️ Renvoyer la vue avec l'erreur pour informer l'utilisateur  
            }

            // 🛠️ Étape 2 : Vérifier que tous les champs obligatoires sont remplis  
            // ➡️ Quels champs doivent être vérifiés ?  
            // ➡️ Quelle méthode permet de vérifier si une chaîne est vide ou contient seulement des espaces ?  
            // ➡️ Si un champ est vide, comment afficher un message d'erreur ?  
            // ➡️ Où doit-on stocker ces erreurs pour qu'elles soient affichées sur la page ?

            // 🛠️ Vérifier `Name`  
            // ➡️ Comment vérifier si `order.Name` est vide ou contient uniquement des espaces ?  
            // ➡️ Si c'est le cas, afficher un message d'erreur et l'ajouter à `ModelState`.  
            // ➡️ Tester en soumettant le formulaire avec un champ vide.  
            var name = order.Name;

            if (string.IsNullOrWhiteSpace(name)) {
                Debug.WriteLine("❌ Le champs name est vide");
                ModelState.AddModelError("", _localizer["ErrorMissingName"]);
                return View(order);
            }

            // 🛠️ Vérifier `Address`

            var adresse = order.Address;

            if (string.IsNullOrWhiteSpace(adresse))
            {
                Debug.WriteLine("❌ Le champs adresse est vide");
                ModelState.AddModelError("", _localizer["ErrorMissingAddress"]);
                return View(order);
            }


            // 🛠️ Vérifier `City`

            var city = order.City;

            if (string.IsNullOrWhiteSpace(city))
            {
                Debug.WriteLine("❌ Le champs name est vide");
                ModelState.AddModelError("", _localizer["ErrorMissingCity"]);
                return View(order);
            }


            // 🛠️ Vérifier `Country`

            var country = order.Country;

            if (string.IsNullOrWhiteSpace(country))
            {
                Debug.WriteLine("❌ Le champs name est vide");
                ModelState.AddModelError("", _localizer["ErrorMissingCountry"]);
                return View(order);
            }



            // 🛠️ Vérifier s'il y a des erreurs avant de continuer  
            // ➡️ Quelle propriété permet de savoir si des erreurs ont été ajoutées ?  
            // ➡️ Si des erreurs sont présentes, afficher un message et renvoyer la vue.


            // 🛠️ Étape 3 : Ajouter les articles du panier à la commande
            // ➡️ Où sont stockés les produits dans `_cart` ?
            // ➡️ Comment les ajouter dans `order.Lines` ?

            // 🛠️ Étape 4 : Enregistrer la commande
            // ➡️ Quelle méthode permet de sauvegarder la commande dans `_orderService` ?
            // ➡️ À quel moment faut-il l'appeler ?

            // 🛠️ Étape 5 : Rediriger vers la page de confirmation
            // ➡️ Quelle action doit être appelée après avoir sauvegardé la commande ?


        }

        public ViewResult Completed()
        {
            // 🛠️ Étape 6 : Vider le panier après la commande
            // ➡️ Quelle méthode permet de supprimer tous les éléments du panier ?
            // ➡️ À quel moment doit-on l'appeler pour s'assurer que le panier est bien vidé ?

        }
    }
}
