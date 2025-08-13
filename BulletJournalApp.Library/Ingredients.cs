using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BulletJournalApp.Library
{
    public class Ingredients
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Measurements { get; set; }

        public Ingredients (string name, int quantity, double price, string measurements)
        {
            ValidateString(name, nameof(name));
            ValidateQuantity(quantity);
            ValidatePrice(price);
            ValidateMeasurements(measurements);
            Name = name;
            Quantity = quantity;
            Price = Math.Round(price, 2);
            Measurements = measurements;
        }

        public void Update(string newName, int newQuantity, double newPrice, string newMeasurements)
        {
            ValidateString(newName, nameof(newName));
            ValidateQuantity(newQuantity);
            ValidatePrice(newPrice);
            ValidateMeasurements(newMeasurements);
            Name = newName;
            Quantity = newQuantity;
            Price = newPrice;
            Measurements = newMeasurements;
        }

        public void ValidateString(string input, string fieldname)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException($"{fieldname} cannot be blank or null");
        }

        public void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException($"Invalid quantity number. {quantity} is not greater than zero");
        }

        public void ValidatePrice(double price)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException($"Invalid price. {price} must be greater than zero or equal to zero");
        }
        public void ValidateMeasurements(string measurements)
        {
            measurements = measurements.ToLower();
            if (!measurements.Contains("tbsp") &&
                !measurements.Contains("tsp") &&
                !measurements.Contains("g") &&
                !measurements.Contains("lbs") &&
                !measurements.Contains("oz") &&
                !measurements.Contains("ml") &&
                !measurements.Contains("gallon") &&
                !measurements.Contains("gallons") &&
                !measurements.Contains("quart") &&
                !measurements.Contains("quarts") &&
                !measurements.Contains("pint") &&
                !measurements.Contains("pints") &&
                !measurements.Contains("cup") &&
                !measurements.Contains("cups") &&
                !measurements.Contains("liter") &&
                !measurements.Contains("liters") &&
                !measurements.Contains("n/a")
                )
            {
                throw new FormatException($"Invalid measurement. {measurements} is not valid measurement");
            }
        }
    }
}
