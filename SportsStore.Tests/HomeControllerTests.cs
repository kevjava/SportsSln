using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests;

public class HomeControllerTests
{
    [Fact]
    public void CanUseRepository()
    {
        // Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns(
            (
                new Product[]
                {
                    new Product { ProductId = 1, Name = "P1" },
                    new Product { ProductId = 2, Name = "P2" }
                }
            ).AsQueryable<Product>()
        );

        HomeController controller = new HomeController(mock.Object);

        // Act
        var viewResult = controller.Index() as ViewResult;
        var model = viewResult?.ViewData.Model as ProductsListViewModel;
        IEnumerable<Product>? result = model?.Products as IEnumerable<Product>;

        // Assert
        Product[] prodArray = result?.ToArray() ?? Array.Empty<Product>();
        Assert.Equal(2, prodArray.Length);
        Assert.Equal(1, prodArray[0].ProductId);
        Assert.Equal("P1", prodArray[0].Name);
        Assert.Equal(2, prodArray[1].ProductId);
        Assert.Equal("P2", prodArray[1].Name);
    }

    [Fact]
    public void CanPaginate()
    {
        // Arrange
        Mock<IStoreRepository> mock = new();
        mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductId = 1, Name = "P1"},
            new Product {ProductId = 2, Name = "P2"},
            new Product {ProductId = 3, Name = "P3"},
            new Product {ProductId = 4, Name = "P4"},
            new Product {ProductId = 5, Name = "P5"}
        }).AsQueryable<Product>());

        HomeController controller = new(mock.Object);
        controller.PageSize = 3;

        // Act
        var model = (controller.Index(2) as ViewResult)?.ViewData.Model as ProductsListViewModel;
        IEnumerable<Product> result = model?.Products as IEnumerable<Product> ?? Enumerable.Empty<Product>();

        // Assert
        Product[] prodArray = result.ToArray();
        Assert.Equal(2, prodArray.Length);
        Assert.Equal("P4", prodArray[0].Name);
        Assert.Equal("P5", prodArray[1].Name);
    }

    [Fact]
    public void Can_Send_Pagination_View_Model()
    {
        // Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductId = 1, Name = "P1"},
            new Product {ProductId = 2, Name = "P2"},
            new Product {ProductId = 3, Name = "P3"},
            new Product {ProductId = 4, Name = "P4"},
            new Product {ProductId = 5, Name = "P5"}
        }).AsQueryable<Product>());
        
        // Arrange
        HomeController controller = new(mock.Object) { PageSize = 3 };

        // Act
        ProductsListViewModel result = controller.Index(2)?.ViewData.Model as ProductsListViewModel ?? new();

        // Assert
        PagingInfo pageInfo = result.PagingInfo;
        Assert.Equal(2, pageInfo.CurrentPage);
        Assert.Equal(3, pageInfo.ItemsPerPage);
        Assert.Equal(5, pageInfo.TotalItems);
        Assert.Equal(2, pageInfo.TotalPages);
    }
}