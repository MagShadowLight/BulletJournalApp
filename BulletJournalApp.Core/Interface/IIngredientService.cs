using BulletJournalApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Core.Interface
{
    public interface IIngredientService
    {
        public void AddIngredient(Ingredients ingredients);
        public List<Ingredients> GetAllIngredients();
        public void ChangeIngredients(string oldName, string newName, int newQuantity, double newPrice, string newMeasurement);
        public void DeleteIngredient(string name);
        public Ingredients FindIngredientsByName(string name);
    }
}
