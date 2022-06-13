using System.Linq;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests {

    public class CartTests {

        [Fact]
        public void Can_Add_New_Lines() {
            Product p1 = new Product{ ProductId = 1, Name = "P1" };
            Product p2 = new Product{ ProductId = 2, Name = "P2" };

            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines() {
            Product p1 = new Product{ ProductId = 1, Name = "P1" };
            Product p2 = new Product{ ProductId = 2, Name = "P2" };

            Cart cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 10); // Add same item twice.

            CartLine[] results = (cart.Lines ?? new()).OrderBy(c => c.Product.ProductId).ToArray();
            
            Assert.Equal(2, results.Length);
            Assert.Equal(1, results[0].Product.ProductId);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(2, results[1].Product.ProductId);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line() {
            Product p1 = new Product{ ProductId = 1, Name = "P1" };
            Product p2 = new Product{ ProductId = 2, Name = "P2" };
            Product p3 = new Product{ ProductId = 3, Name = "P3" };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 4);
            cart.AddItem(p2, 6);

            cart.RemoveLine(p2);

            CartLine[] results = (cart.Lines ?? new()).OrderBy(c => c.Product.ProductId).ToArray();

            Assert.Equal(2, results.Length);

            Assert.Equal(1, results[0].Product.ProductId);
            Assert.Equal(2, results[0].Quantity);

            Assert.Equal(3, results[1].Product.ProductId);
            Assert.Equal(4, results[1].Quantity);
        }

        [Fact]
        public void Can_Calculate_Cart_Total() {
            Product p1 = new Product{ ProductId = 1, Name = "P1", Price = 1M };
            Product p2 = new Product{ ProductId = 2, Name = "P2", Price = 10M };

            Cart cart = new Cart();

            cart.AddItem(p1, 3);
            cart.AddItem(p2, 6);
            cart.AddItem(p1, 6);

            decimal result = cart.ComputeTotalValue();
            Assert.Equal(69M, result); // Nice.
        }

        [Fact]
        public void Can_Clear_Cart() {
            Product p1 = new Product{ ProductId = 1, Name = "P1" };
            Product p2 = new Product{ ProductId = 2, Name = "P2" };
            Product p3 = new Product{ ProductId = 3, Name = "P3" };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 4);
            cart.AddItem(p2, 6);

            cart.Clear();

            Assert.Empty(cart.Lines);
        }
    }

}