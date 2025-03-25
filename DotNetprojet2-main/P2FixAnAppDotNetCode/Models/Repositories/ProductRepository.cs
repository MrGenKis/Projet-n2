using P2FixAnAppDotNetCode.Models.Repositories;
using P2FixAnAppDotNetCode.Models;
using System.Linq;
using System.Collections.Generic;
using System;

public class ProductRepository : IProductRepository
{
    // Liste statique qui simule un "stock" de produits en mémoire
    private static readonly List<Product> _products;

    static ProductRepository()
    {
        // On remplit la liste _products une seule fois (constructeur statique)
        _products = new List<Product>
        {
            new Product(1, 10, 92.50, "Echo Dot", "(2nd Generation) - Black"),
            new Product(2, 20, 9.99, "Anker 3ft / 0.9m Nylon Braided", "Tangle-Free Micro USB Cable"),
            new Product(3, 30, 69.99, "JVC HAFX8R Headphone", "Riptidz, In-Ear"),
            new Product(4, 40, 32.50, "VTech CS6114 DECT 6.0", "Cordless Phone"),
            new Product(5, 50, 895.00, "NOKIA OEM BL-5J", "Cell Phone ")
        };
    }

    // Retourne uniquement les produits qui ont encore du stock
    public Product[] GetAllProducts()
    {
        return _products
            .Where(p => p.Stock > 0)
            .OrderBy(p => p.Name)
            .ToArray();
    }

    // Mise à jour du stock en mémoire 
    public void UpdateProductStocks(int productId, int quantity)
    {
        
        var product = _products.FirstOrDefault(p => p.Id == productId);
        if (product != null)
        {
            product.Stock -= quantity;

            
            if (product.Stock < 0)
                product.Stock = 0;

        
            Console.WriteLine($"[DEBUG] Stock mis à jour pour {product.Name} : {product.Stock} restant.");
        }
    }
}
