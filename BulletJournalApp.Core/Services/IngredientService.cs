using BulletJournalApp.Core.Interface;
using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Services
{
    public class IngredientService : IIngredientService
    {
        
        public List<Ingredients> ingredients = new();
        public IngredientService()
        {

        }

        public void AddIngredient(Ingredients ingredient)
        {
            ValidateDupe(ingredient.Name);
            ingredients.Add(ingredient);
        }

        public void ChangeIngredients(string oldName, string newName, int newQuantity, double newPrice, string newMeasurement)
        {
            var ingredient = FindIngredientsByName(oldName);
            if (ingredient == null)
                throw new ArgumentNullException($"Ingredient: {oldName} not found");
            ValidateDupe(newName);
            ingredient.Update(newName, newQuantity, newPrice, newMeasurement);
        }

        public void DeleteIngredient(string name)
        {
            var ingredient = FindIngredientsByName(name);
            if (ingredient == null)
                throw new ArgumentNullException($"Ingredient: {name} not found");
            ingredients.Remove(ingredient);
        }

        public Ingredients FindIngredientsByName(string name)
        {
            return ingredients.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Ingredients> GetAllIngredients()
        {
            return ingredients;
        }

        public void ValidateDupe(string name)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient.Name.Equals(name))
                    throw new DuplicateNameException($"This Ingredient is duplication of {name} found in {ingredients.IndexOf(ingredient)} index");
            }
        }
    }
}
