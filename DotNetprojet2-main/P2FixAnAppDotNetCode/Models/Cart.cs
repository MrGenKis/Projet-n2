using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        // 🛠️ Étape 1 : Déclarer une liste `_cartLines` qui stockera les produits du panier.
        // ➡️ Créer une liste privée `_cartLines` pour stocker les produits de façon persistante.
        // ➡️ Assure-toi d'**initialiser** la liste pour éviter des erreurs (`null`).
        private readonly List<CartLine> _cartLines = new List<CartLine>();



        // 🛠️ Étape 2 : Modifier `Lines` pour qu'il retourne `_cartLines`.
        // ➡️ Actuellement, `Lines` appelle `GetCartLineList()` qui recrée une liste vide.
        // ➡️ Il faut changer cela pour utiliser la liste `_cartLines`.

        public IEnumerable<CartLine> Lines => _cartLines;




        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>
        public void AddItem(Product product, int quantity)
        {
            Debug.WriteLine("✅ La méthode AddItem() a été appelée !");

            // 🛠️ Étape 3 : Vérifier si le produit est valide avant de continuer.
            // ➡️ Si `product` est `null`, afficher un message d'erreur et arrêter la méthode.

            if (product == null) {

                Debug.WriteLine("Erreur");
                return;
            }
    


            // 🛠️ Étape 4 : Vérifier si le produit est déjà présent dans `_cartLines`.
            // ➡️ Parcourir `_cartLines` avec une boucle `foreach`.
            // ➡️ Comparer `line.Product.Id` avec `product.Id`.

            foreach ( var line in _cartLines)
            {
                if (line.Product.Id == product.Id) {

                    // 🛠️ Étape 5 : Si le produit est trouvé, augmenter sa quantité.
                    // ➡️ Incrémenter `line.Quantity` de la quantité ajoutée.
                    // ➡️ Afficher un message `Debug.WriteLine()` pour vérifier la mise à jour.
                    // ➡️ Arrêter la méthode (`return;`) après avoir mis à jour la quantité.

                    line.Quantity += quantity;
                    Debug.WriteLine("quantité mis a jours");
                    return ;
                }
               
   
            }

            
                // 🛠️ Étape 6 : Si le produit n'est pas trouvé, l'ajouter à `_cartLines`.
                // ➡️ Créer un nouvel objet `CartLine` avec `Product` et `Quantity`.
                // ➡️ Ajouter cet objet à `_cartLines`.
                // ➡️ Afficher un message `Debug.WriteLine()` pour confirmer l'ajout.

                CartLine newLine = new CartLine
                {
                    Product = product,
                    Quantity = quantity
                };

                _cartLines.Add(newLine);
                Debug.WriteLine($"🛒 Produit ajouté : {product.Name}, Quantité : {quantity}");


        }

        /// <summary>
        /// Removes a product from the cart
        /// </summary>
        public void RemoveLine(Product product)
        {
            // 🛠️ Étape 1 : Vérifier si `product` est valide
            // ➡️ Si `product` est `null`, afficher un message d'erreur avec `Debug.WriteLine()`.
            // ➡️ Arrêter la méthode (`return;`) pour éviter de traiter un produit invalide.
            if (product == null)
            {
                // Ajoute un message Debug pour indiquer que le produit est invalide.
                Debug.WriteLine("Produit invalide");
                return;
            }

            // 🛠️ Étape 2 : Vérifier si le produit existe dans `_cartLines`
            // ➡️ Utiliser `.FirstOrDefault()` pour chercher une `CartLine` qui contient `product.Id`.
            // ➡️ Où faut-il appliquer cette recherche ?
            // ➡️ Quelle propriété d’un `CartLine` contient l’ID du produit ?
            // ➡️ Quelle condition doit être utilisée pour comparer les ID ?
            var cartLine = _cartLines.FirstOrDefault(line => line.Product.Id == product.Id);

            // 🛠️ Étape 3 : Vérifier si le produit est trouvé
            // ➡️ Si `cartLine` est `null`, afficher un message indiquant que le produit n'est pas dans le panier.
            // ➡️ Arrêter la méthode (`return;`) si le produit n'est pas trouvé.
            if (cartLine == null)
            {
                // ➡️ Quel message clair peux-tu afficher pour indiquer que le produit n’est pas trouvé dans le panier ?
                Debug.WriteLine("Le produit n'a pas été trouvé dans le panier.");
                return;
            }

            // 🛠️ Étape 4 : Vérifier la quantité du produit dans le panier
            // ➡️ Vérifier si la quantité du produit (`cartLine.Quantity`) est supérieure à 1.
            // ➡️ Si c'est le cas, décrémente simplement la quantité de 1.
            // ➡️ Sinon, si la quantité est égale à 1, supprimer complètement la `CartLine` du panier.
            // ➡️ Quelle instruction permet de réduire une valeur de 1 ?
            // ➡️ Quelle méthode permet de supprimer complètement un élément d'une liste ?

            if (cartLine.Quantity > 1)
            {
                cartLine.Quantity -= 1;
                Debug.WriteLine($" Le produit {product.Name} a bien été éte reduit de une quantité");
            }
            else if(cartLine.Quantity == 1)
            {
                _cartLines.Remove(cartLine);
                Debug.WriteLine($" Le produit {product.Name} a bien été supprimer du produit");
            }

            // 🛠️ Étape 5 : Afficher un message de confirmation selon le cas
            // ➡️ Utilise `Debug.WriteLine()` pour indiquer que la quantité a été réduite d'un produit ou qu'un produit a été complètement supprimé.

           

            // 🛠️ Étape 6 : Vérifier si le panier est vide après la suppression
            // ➡️ Quelle propriété ou méthode permet de vérifier si une liste est vide ?
            // ➡️ Si le panier est vide, affiche un message indiquant que le panier est désormais vide.

            bool test = _cartLines.Any();


            if (test == false)
            {
                Debug.WriteLine("La panier est vide");
            }

            // (Ajoute tes vérifications et conditions ici, sans réponses données !)
        }


        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            // 🛠️ Étape 1 : Vérifier si le panier est vide.
            // ➡️ Comment savoir si `_cartLines` contient des éléments ?
            // ➡️ Si le panier est vide, afficher un message et retourner 0.

           

            if (!_cartLines.Any())
            {
                Debug.WriteLine("Le panier est vide");
                return 0;
            }


            // 🛠️ Étape 2 : Initialiser une variable pour stocker la valeur totale.
            // ➡️ Quelle valeur initiale doit avoir cette variable ?

            double totale = 0;


            // 🛠️ Étape 3 : Parcourir chaque `CartLine` dans `_cartLines`.
            // ➡️ Quelle structure de boucle peux-tu utiliser pour parcourir la liste ?
            foreach(var car in _cartLines)
            {
                // 🛠️ Étape 4 : Calculer la valeur de chaque ligne du panier.
                // ➡️ Comment obtenir le prix d’un produit ?
                // ➡️ Comment obtenir la quantité de ce produit ?
                // ➡️ Comment calculer la valeur totale pour une seule ligne ?

                double prix = car.Product.Price;
                double quatité = car.Quantity;

                double prixproduit = prix * quatité;

                // 🛠️ Étape 5 : Ajouter cette valeur au total.
                // ➡️ Comment accumuler la valeur totale dans la variable que tu as créée ?

                totale += prixproduit;

            }

            // 🛠️ Étape 6 : Afficher la valeur totale calculée avant de la retourner.
            // ➡️ Utilise `Debug.WriteLine()` pour afficher `totale`.
            // ➡️ Quel message clair peux-tu afficher pour indiquer que c'est le total du panier ?

            Debug.WriteLine($"Le prix total du panier est de : {totale}");

            return totale;



        }


        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            // 🛠️ Étape 1 : Vérifier si le panier est vide.
            // ➡️ Comment savoir si `_cartLines` contient des éléments ?
            // ➡️ Si le panier est vide, afficher un message avec `Debug.WriteLine()` et retourner 0.

            if (!_cartLines.Any())
            {
                Debug.WriteLine("Le panier est vide");
                return 0;
            }


            // 🛠️ Étape 2 : Obtenir la valeur totale du panier.
            // ➡️ Quelle méthode as-tu déjà créée qui calcule la valeur totale du panier ?
            // ➡️ Appelle cette méthode et stocke son résultat dans une variable.

            var valeurPanier = GetTotalValue();



            // 🛠️ Étape 3 : Compter le nombre total de produits (quantité totale de tous les articles).
            // ➡️ Parcourir chaque `CartLine` dans `_cartLines`.
            // ➡️ Additionner les quantités (`line.Quantity`) pour obtenir le total de produits.
            // ➡️ Stocker ce total dans une variable.

            double quantité = 0;

            foreach (var line in _cartLines)
            {

                quantité += line.Quantity;
                
            }


            // 🛠️ Étape 4 : Calculer la valeur moyenne.
            // ➡️ Comment calculer une moyenne ? (valeur totale divisée par nombre total de produits)
            // ➡️ Assure-toi de gérer le cas où le total de produits est égal à zéro pour éviter une division par zéro.
            // ➡️ Stocker le résultat dans une variable `average`.

            double average = 0;

            if (quantité == 0)
            {
                Debug.WriteLine("Erreur rien dans le panier");
                return 0;
            }
         
                average = valeurPanier / quantité;


                // 🛠️ Étape 5 : Afficher la valeur moyenne avant de la retourner.
                // ➡️ Utilise `Debug.WriteLine()` pour afficher la valeur moyenne (`average`).
                // ➡️ Quel message clair peux-tu afficher pour indiquer qu'il s'agit de la moyenne des produits du panier ?



                Debug.WriteLine($"La moyenne du panier est de {average}");
           



            // 🛠️ Étape 6 : Retourner la valeur moyenne calculée.
            return average;
        }


        /// <summary>
        /// Looks after a given product in the cart and returns it if found
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // 🛠️ Étape 1 : Vérifier si le panier est vide.
            // ➡️ Comment savoir si `_cartLines` contient des éléments ?
            // ➡️ Si le panier est vide, afficher un message avec `Debug.WriteLine()` et retourner `null`.
            if (!_cartLines.Any())
            {
                Debug.WriteLine("Le panier est vide");
                return null;
            }


            // 🛠️ Étape 2 : Rechercher le produit dans `_cartLines`.
            // ➡️ Quelle méthode peux-tu utiliser pour trouver un élément spécifique dans une liste en C# ?
            // ➡️ Utilise `.FirstOrDefault()` pour rechercher une `CartLine` dont le produit correspond à l'ID donné (`productId`).
            // ➡️ Quelle condition peux-tu utiliser dans `.FirstOrDefault()` pour comparer l'ID du produit ?
           
            var produit = _cartLines.FirstOrDefault(line => line.Product.Id == productId);



            // 🛠️ Étape 3 : Vérifier si le produit a été trouvé.
            // ➡️ Si le produit n'est pas trouvé (`null`), afficher un message avec `Debug.WriteLine()` et retourner `null`.
            // ➡️ Quel message clair peux-tu afficher pour indiquer que le produit n’est pas trouvé dans le panier ?

            if (produit == null)
            {
                Debug.WriteLine("Le produit n'a pas ete trouve");
                return null;
            }



            // 🛠️ Étape 4 : Retourner le produit trouvé.
            // ➡️ Si le produit a été trouvé, retourner le produit (`Product`) contenu dans la `CartLine` trouvée.
            return produit.Product;

        }


        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            // 🛠️ Étape 1 : Vérifier que l'index est valide.
            // ➡️ Comment peux-tu vérifier que l'index ne dépasse pas la taille de la liste ?
            // ➡️ Utilise une condition pour vérifier si l'index est inférieur à 0 ou supérieur ou égal à `_cartLines.Count`.
            // ➡️ Si l'index n'est pas valide, affiche un message avec `Debug.WriteLine()` et retourne `null`.

            // (Fais ta vérification ici)
            if (index < 0 || index >= _cartLines.Count)
            {
                Debug.WriteLine("l'index n'es pas valide");
                return null;

            }



            // 🛠️ Étape 2 : Si l'index est valide, retourne la `CartLine` correspondante.
            // ➡️ Comment accéder à un élément précis d'une liste en utilisant son index en C# ?
            // ➡️ Retourne l'élément de `_cartLines` correspondant à l'index.

            // (Retourne l'élément ici)
            return _cartLines[index];

        }

        /// <summary>
        /// Clears the cart of all added products
        /// </summary>
        public void Clear()
        {
            // 🛠️ Étape 1 : Vérifier si le panier contient des éléments.
            // ➡️ Comment peux-tu vérifier si `_cartLines` est vide ou non ?
            // ➡️ Si le panier est déjà vide, affiche un message avec `Debug.WriteLine()` et arrête la méthode (`return;`).

            // (Ajoute ta vérification ici)
            if (!_cartLines.Any())
            {
                Debug.WriteLine("Le panier est vide");
                return;
            }


            // 🛠️ Étape 2 : Vider complètement le panier.
            // ➡️ Quelle méthode de la classe `List` peux-tu utiliser pour supprimer tous les éléments d'une liste en une seule opération ?
            // ➡️ Applique cette méthode à `_cartLines`.

            _cartLines.Clear();


            // 🛠️ Étape 3 : Vérifier que le panier est bien vidé.
            // ➡️ Ajoute un message avec `Debug.WriteLine()` pour confirmer que le panier a été vidé.

            Debug.WriteLine("La panier est bien vide");
        }

    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
