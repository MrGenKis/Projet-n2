using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
            // ➡️ Cette méthode va chercher la première ligne du panier contenant le produit.
            // ➡️ Où faut-il appliquer cette recherche ?
            // ➡️ Quelle propriété d’un `CartLine` contient l’ID du produit ?
            // ➡️ Quelle condition doit être utilisée pour comparer les ID ?

            var cartLine = _cartLines.FirstOrDefault(line => line.Product.Id == product.Id);



            // 🛠️ Vérifier si `cartLine` est `null` (le produit n'est pas trouvé dans le panier).
            // ➡️ Si `cartLine` est `null`, afficher un message avec `Debug.WriteLine()`.
            // ➡️ Quel message afficher pour indiquer que le produit ne se trouve pas dans le panier ?
            // ➡️ Arrêter la méthode (`return;`) pour éviter d’exécuter le reste du code inutilement.

            if ( cartLine == null )
            {
                Debug.WriteLine("Le produit n'a pas ete trouver dans le panier"); // ➡️ Ajouter un message Debug pour indiquer que le produit n’a pas été trouvé dans le panier.
                return;
            }else
            {
                // 🛠️ Étape 3 : Supprimer la `CartLine` du panier
                // ➡️ Si la ligne a été trouvée, la retirer de `_cartLines`.
                // ➡️ Utiliser `.Remove()` pour supprimer l'élément de la liste.


                _cartLines.Remove(cartLine);

                // 🛠️ Étape 4 : Afficher un message de confirmation
                // ➡️ Utiliser `Debug.WriteLine()` pour indiquer qu'un produit a bien été supprimé du panier.
                Debug.WriteLine($" Le produit {product.Name} a bien été supprimer du produit");

                // 🛠️ Étape 5 : Vérifier si le panier est vide après la suppression
                // ➡️ Si `_cartLines` ne contient plus aucun élément, afficher un message indiquant que le panier est désormais vide.
                // ➡️ Quelle propriété ou méthode permet de vérifier si une liste est vide ?

                bool test =_cartLines.Any();

                
                if(test == false)
                {
                    Debug.WriteLine("La panier est vide");
                }

            }







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



            // 🛠️ Étape 2 : Obtenir la valeur totale du panier.
            // ➡️ Quelle méthode as-tu déjà créée qui calcule la valeur totale du panier ?
            // ➡️ Appelle cette méthode et stocke son résultat dans une variable.



            // 🛠️ Étape 3 : Compter le nombre total de produits (quantité totale de tous les articles).
            // ➡️ Parcourir chaque `CartLine` dans `_cartLines`.
            // ➡️ Additionner les quantités (`line.Quantity`) pour obtenir le total de produits.
            // ➡️ Stocker ce total dans une variable.



            // 🛠️ Étape 4 : Calculer la valeur moyenne.
            // ➡️ Comment calculer une moyenne ? (valeur totale divisée par nombre total de produits)
            // ➡️ Assure-toi de gérer le cas où le total de produits est égal à zéro pour éviter une division par zéro.
            // ➡️ Stocker le résultat dans une variable `average`.



            // 🛠️ Étape 5 : Afficher la valeur moyenne avant de la retourner.
            // ➡️ Utilise `Debug.WriteLine()` pour afficher la valeur moyenne (`average`).
            // ➡️ Quel message clair peux-tu afficher pour indiquer qu'il s'agit de la moyenne des produits du panier ?



            // 🛠️ Étape 6 : Retourner la valeur moyenne calculée.

        }


        /// <summary>
        /// Looks after a given product in the cart and returns it if found
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // 🛠️ Étape 10 : Trouver un produit dans `_cartLines`.
            // ➡️ Utiliser `.FirstOrDefault()` pour chercher un produit par `productId`.
            // ➡️ Retourner `Product` s'il existe, sinon `null`.

            return null; // ❌ Remplacer par la bonne recherche.
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            // 🛠️ Étape 11 : Récupérer une ligne spécifique du panier.
            // ➡️ Vérifier que l'index est valide.
            // ➡️ Retourner `CartLine` correspondant.

            return Lines.ToArray()[index]; // ❌ Vérifier que l'index ne dépasse pas la taille de la liste.
        }

        /// <summary>
        /// Clears the cart of all added products
        /// </summary>
        public void Clear()
        {
            // 🛠️ Étape 12 : Vider complètement le panier.
            // ➡️ Supprimer tous les éléments de `_cartLines`.

        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
