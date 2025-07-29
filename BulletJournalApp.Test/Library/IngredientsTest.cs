using BulletJournalApp.Library;
using BulletJournalApp.Test.Data.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Library
{
    public class IngredientsTest
    {
        [Theory]
        [MemberData(nameof(IngredientsFixture.GetValidIngredients), MemberType = typeof(IngredientsFixture))]
        public void Given_There_Are_Valid_Values_When_Trying_To_Add_Ingredient_Then_Those_Values_Should_Assign(string name, int quantity, double price, string measurement, string newname, int newquantity, double newprice, string newmeasurement)
        {
            // Arrange // Act
            var ingredient = new Ingredients(name, quantity, price, measurement);
            // Assert
            Assert.Equal(name, ingredient.Name);
            Assert.Equal(quantity, ingredient.Quantity);
            Assert.Equal(price, ingredient.Price);
            Assert.Equal(measurement, ingredient.Measurements);
        }
        [Theory]
        [MemberData(nameof(IngredientsFixture.GetIngredientsWithEmptyString), MemberType = typeof(IngredientsFixture))]
        public void Given_There_Are_Values_With_Empty_Name_When_Trying_To_Add_Ingredient_Then_It_Should_Throw_Exception(string name, string fakename, int quantity, double price, string measurement, string newname, int newquantity, double newprice, string newmeasurement)
        {
            Assert.Throws<ArgumentNullException>(() => { new Ingredients(name, quantity, price, measurement); });
        }
        [Theory]
        [MemberData(nameof(IngredientsFixture.GetIngredientsWithOutOfRangeValue), MemberType = typeof(IngredientsFixture))]
        public void Given_There_Are_Values_With_Out_Of_Range_Value_When_Trying_To_Add_Ingredient_Then_It_Should_Throw_Exception(string name, int quantity, double price, int fakequantity, double fakeprice, string measurement, string newname, int newquantity, double newprice, string newmeasurement)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { new Ingredients(name, quantity, price, measurement); });
        }
        [Theory]
        [MemberData(nameof(IngredientsFixture.GetIngredientsWithIncorrectFormatValue), MemberType = typeof(IngredientsFixture))]
        public void Given_There_Are_Values_With_Incorrect_Format_Value_When_Trying_To_Add_Ingredient_Then_It_Should_Throw_Exception(string name, int quantity, double price, string measurement, string fakemeasurement, string newname, int newquantity, double newprice, string newmeasurement)
        {
            Assert.Throws<FormatException>(() => { new Ingredients(name, quantity, price, measurement); });
        }
        [Theory]
        [MemberData(nameof(IngredientsFixture.GetValidIngredients), MemberType = typeof(IngredientsFixture))]
        public void Given_There_Are_Ingredient_When_Trying_To_Update_Ingredient_With_Valid_Value_Then_Those_Values_Should_Reassign(string name, int quantity, double price, string measurement, string newname, int newquantity, double newprice, string newmeasurement)
        {
            // Arrange
            var ingredient = new Ingredients(name, quantity, price, measurement);
            // Act
            ingredient.Update(newname, newquantity, newprice, newmeasurement);
            // Assert
            Assert.Equal(newname, ingredient.Name);
            Assert.Equal(newquantity, ingredient.Quantity);
            Assert.Equal(newprice, ingredient.Price);
            Assert.Equal(newmeasurement, ingredient.Measurements);
        }
        [Theory]
        [MemberData(nameof(IngredientsFixture.GetIngredientsWithEmptyString), MemberType = typeof(IngredientsFixture))]
        public void Given_There_Are_Ingredient_When_Trying_To_Update_Ingredient_With_Empty_String_Then_It_Should_Throw_Exception(string name, string fakename, int quantity, double price, string measurement, string newname, int newquantity, double newprice, string newmeasurement)
        {
            // Arrange
            var ingredient = new Ingredients(fakename, quantity, price, measurement);
            // Act // Assert
            Assert.Throws<ArgumentNullException>(() => ingredient.Update(newname, quantity, newprice, newmeasurement));
        }
        [Theory]
        [MemberData(nameof(IngredientsFixture.GetIngredientsWithOutOfRangeValue), MemberType = typeof(IngredientsFixture))]
        public void Given_There_Are_Ingredient_When_Trying_To_Update_Ingredient_With_Out_Of_Range_Values_Then_It_Should_Throw_Exception(string name, int quantity, double price, int fakequantity, double fakeprice, string measurement, string newname, int newquantity, double newprice, string newmeasurement)
        {
            // Arrange
            var ingredient = new Ingredients(name, fakequantity, fakeprice, measurement);
            // Act // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ingredient.Update(newname, newquantity, newprice, measurement));
        }
        [Theory]
        [MemberData(nameof(IngredientsFixture.GetIngredientsWithIncorrectFormatValue), MemberType = typeof(IngredientsFixture))]
        public void Given_There_Are_Ingredient_When_Trying_To_Update_Ingredient_With_Invalid_Props_Then_It_Should_Throw_Exception(string name, int quantity, double price, string measurement, string fakemeasurement, string newname, int newquantity, double newprice, string newmeasurement)
        {
            // Arrange
            var ingredient = new Ingredients(name, quantity, price, fakemeasurement);
            // Act // Assert
            Assert.Throws<FormatException>(() =>  ingredient.Update(newname, newquantity, newprice, measurement));
        }
    }
}
