using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using P2FixAnAppDotNetCode.Models.Repositories;

namespace P2FixAnAppDotNetCode.Models.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all products from the inventory
        /// </summary>
        public Product[] GetAllProducts()
        {
            return _productRepository.GetAllProducts(); // Retourne un tableau, comme demandé par l'interface
        }


        /// <summary>
        /// Get a product from the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {
            // 🛠️ Étape 1 : Récupérer la liste des produits.
            // ➡️ Quelle méthode peux-tu appeler pour obtenir la liste des produits ?

            var listProduit = GetAllProducts();

            // 🛠️ Étape 2 : Chercher un produit dans la liste en fonction de l'ID.
            // ➡️ Comment peut-on chercher un élément dans une liste en C# ?
            // ➡️ Regarde la méthode `.FirstOrDefault()`.

            var product = listProduit.FirstOrDefault(p => p.Id == id); 

            // 🛠️ Étape 3 : Retourner le produit trouvé ou `null` si aucun produit ne correspond.
            // ➡️ Si un produit est trouvé, on le retourne.
            // ➡️ Sinon, on retourne `null`.

            if (product == null) { 
                return null;
            }else {

            return product; // ❌ Remplace `null` par la bonne solution.
            }
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending on ordered quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            // 🛠️ Étape 1 : Vérifier si `cart` est vide.
            // ➡️ Comment vérifier si une liste contient des éléments en C# ?
            // ➡️ Si le panier est vide, affiche un message et arrête la méthode (`return;`).

            if (cart == null || !cart.Lines.Any()) { 
                Console.WriteLine("le panier est vide");
                return; 
            }

            // 🛠️ Étape 2 : Parcourir les articles du panier.
            // ➡️ Utilise une boucle `foreach` pour parcourir `cart.Lines`.

            foreach (var line in cart.Lines)
            {
                Console.WriteLine($"Le nom du produit est : {line.Product.Name}, l'id du produit est {line.Product.Id} et la quantié est {line.Quantity}");

                // 🛠️ Étape 3 : Récupérer chaque produit de la base de données.
                // ➡️ Quelle méthode peux-tu appeler pour obtenir un produit par son ID ?

                var product = GetProductById(line.Product.Id);

                // 🛠️ Étape 4 : Vérifier si le produit existe avant de continuer.
                // ➡️ Si `product` est `null`, afficher un message d’erreur et passer au suivant.

                if (product == null)
                {
                    Console.WriteLine("erreur");
                    continue;
                }
                else
                {
                    //product.Stock est la quantité disponible en magasin.
                    // line.Quantity est la quantité commandée.

                    // 🛠️ Étape 5 : Afficher le stock avant modification
                    Console.WriteLine($"Le produit {product.Name} (ID: {product.Id}) a un stock disponible de : {product.Stock}");

                    // 🛠️ Étape 6 : Réduire directement le stock
                    product.Stock -= line.Quantity;

                    // 🛠️ Étape 7 : Afficher le stock après modification
                    Console.WriteLine($"Nouveau stock du produit {product.Name} (ID: {product.Id}) : {product.Stock}");

                    // 🛠️ Étape 8 : Vérifier si le stock devient négatif
                    // ➡️ Si `product.Stock < 0`, afficher un message d'erreur.
                    // ➡️ Comment éviter que `product.Stock` passe sous 0 ?

                    if (product.Stock < 0)
                    {
                        Console.WriteLine("⚠️ Attention : Stock insuffisant !");
                        product.Stock = 0; // Éviter un stock négatif.
                    }
                }

            }

            

           

        }
    }
}
