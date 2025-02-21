using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// Provides services method to manage the application language
    /// </summary>
    public class LanguageService : ILanguageService
    {
        /// <summary>
        /// Set the UI language
        /// </summary>
        public void ChangeUiLanguage(HttpContext context, string language)
        {
            string culture = SetCulture(language);
            Debug.WriteLine($"🌐 Langue choisie : {language}, Culture appliquée : {culture}");
            UpdateCultureCookie(context, culture);
        }


        /// <summary>
        /// Set the culture
        /// </summary>
        public string SetCulture(string language)
        {
            // 🛠️ Étape 3 : Initialiser une variable pour stocker la culture.
            // ➡️ Quelle valeur initiale donner à `culture` pour être sûr qu’elle sera définie correctement plus tard ?
            string culture = "";

            // 🛠️ Étape 4 : Attribuer la bonne culture en fonction de la langue passée en paramètre.
            // ➡️ Actuellement, ton switch utilise `culture`. Est-ce la bonne variable à comparer ?
            // ➡️ Quelle variable dois-tu utiliser pour décider quelle culture appliquer ? (regarde les paramètres de la méthode)

            switch (language.ToLower()) 
            {
                case "french": // 🛠️ Étape 5 : Vérifier la valeur exacte à comparer
                    // ➡️ Quel est le texte exact passé en paramètre ? "french", "French", "fr" ?
                    culture = "fr"; // ❌ Vérifier si tu modifies la bonne variable ici
                    break;
                case "spanish":
                    culture = "es"; // ❌ Vérifier si tu modifies la bonne variable ici
                    break;
                default:
                    culture = "en"; // ❌ Vérifier si tu modifies la bonne variable ici
                    break;
            }

            // 🛠️ Étape 6 : Vérifier quelle variable tu dois retourner
            // ➡️ Actuellement tu retournes `culture`, mais est-ce que `culture` a été modifiée dans le switch ?
            // ➡️ Quelle variable contient maintenant la bonne culture que tu veux retourner ?

            return culture; // ❌ Remplacer `culture` par la bonne variable
        }

        /// <summary>
        /// Update the culture cookie
        /// </summary>
        public void UpdateCultureCookie(HttpContext context, string culture)
        {
            Debug.WriteLine($"🍪 Mise à jour du cookie avec la culture : {culture}");

            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture))
            );
        }

    }
}
