using ServiceContracts;
using ServiceContracts.DTOs;
using Services;
using Xunit.Abstractions;

namespace UnitTests;

public class StocksServiceTest
{
    private readonly IStocksService _stocksService;
    private readonly ITestOutputHelper _testOutputHelper;

    public StocksServiceTest(ITestOutputHelper testOutputHelper)
    {
        _stocksService = new StocksService();
        _testOutputHelper = testOutputHelper;
    }

    #region CreateBuyOrder
    [Fact]
    public void CreateBuyOrder_RequestIsNull()
    {
        //Arrange
        BuyOrderRequest? buyOrderRequest = null;

        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_QuantityIsLessThanRange()
    {
        //Arrange
        BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            Quantity = 0
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_QuantityIsGreaterThanRange()
    {
        //Arrange
        BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            Quantity = 100001
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_PriceIsLessThanRange()
    {
        //Arrange
        BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            Price = 0
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_PriceIsGreaterThanRange()
    {
        //Arrange
        BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            Price = 10001
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_StockSymbolIsNull()
    {
        //Arrange
        BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = null,
            StockName = "Himalayan Distillery Limited"
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_DateAndTimeOfOrderIsGreater()
    {
        //Arrange
        BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_ValidBuyOrder()
    {
        //Arrange
        BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            DateAndTimeOfOrder = DateTime.Parse("2000-01-01"),
            Quantity = 1,
            Price = 10000
        };

        //Act
        BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);
        List<BuyOrderResponse> buyOrderResponses = _stocksService.GetBuyOrders();

        //Assert
        Assert.True(buyOrderResponse.BuyOrderID != Guid.Empty);
        Assert.Contains(buyOrderResponse, buyOrderResponses);
    }
    #endregion

    #region CreateSellOrder
    [Fact]
    public void CreateSellOrder_RequestIsNull()
    {
        //Arrange
        SellOrderRequest? sellOrderRequest = null;

        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_QuantityIsLessThanRange()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            Quantity = 0
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_QuantityIsGreaterThanRange()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            Quantity = 100001
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_PriceIsLessThanRange()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            Price = 0
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_PriceIsGreaterThanRange()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            Price = 10001
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_StockSymbolIsNull()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = null,
            StockName = "Himalayan Distillery Limited"
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_DateAndTimeOfOrderIsGreater()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _stocksService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_ValidSellOrder()
    {
        //Arrange
        SellOrderRequest sellOrderRequest = new SellOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            DateAndTimeOfOrder = DateTime.Parse("2000-01-01"),
            Quantity = 1,
            Price = 10000
        };

        //Act
        SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);
        List<SellOrderResponse> sellOrderResponses = _stocksService.GetSellOrders();

        //Assert
        Assert.True(sellOrderResponse.SellOrderID != Guid.Empty);
        Assert.Contains(sellOrderResponse, sellOrderResponses);
    }
    #endregion

    #region GetBuyOrders
    [Fact]
    public void GetBuyOrders_EmptyList()
    {
        //Act
        List<BuyOrderResponse> buyOrderResponses = _stocksService.GetBuyOrders();

        //Assert
        Assert.Empty(buyOrderResponses);
    }

    [Fact]
    public void GetBuyOrders_RetrieveAddedOrders()
    {
        //Arrange
        BuyOrderRequest buyOrderRequest1 = new BuyOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            DateAndTimeOfOrder = DateTime.Parse("2000-01-01"),
            Quantity = 1,
            Price = 10000
        };
        BuyOrderRequest buyOrderRequest2 = new BuyOrderRequest()
        {
            StockSymbol = "CBBL",
            StockName = "Chhimek Bikas Bank Limited",
            DateAndTimeOfOrder = DateTime.Parse("2010-01-01"),
            Quantity = 100,
            Price = 9900
        };
        List<BuyOrderRequest> buyOrderRequests = new List<BuyOrderRequest>()
        {
            buyOrderRequest1, buyOrderRequest2
        };
        List<BuyOrderResponse> buyOrderResponses = new List<BuyOrderResponse>();
        foreach (BuyOrderRequest buyOrderRequest in buyOrderRequests)
        {
            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);
            buyOrderResponses.Add(buyOrderResponse);
        }
        _testOutputHelper.WriteLine("Expected:");
        foreach (BuyOrderResponse buyOrderResponse1 in buyOrderResponses)
        {
            _testOutputHelper.WriteLine(buyOrderResponse1.ToString());
        }

        //Act
        List<BuyOrderResponse> buyOrderResponses1 = _stocksService.GetBuyOrders();
        _testOutputHelper.WriteLine("Actual:");
        foreach (BuyOrderResponse buyOrderResponse2 in buyOrderResponses1)
        {
            _testOutputHelper.WriteLine(buyOrderResponse2.ToString());
        }

        //Assert
        foreach (BuyOrderResponse buyOrderResponse3 in buyOrderResponses)
        {
            Assert.Contains(buyOrderResponse3, buyOrderResponses1);
        }
    }
    #endregion

    #region GetSellOrders
    [Fact]
    public void GetSellOrders_EmptyList()
    {
        //Act
        List<SellOrderResponse> sellOrderResponses = _stocksService.GetSellOrders();

        //Assert
        Assert.Empty(sellOrderResponses);
    }

    [Fact]
    public void GetSellOrders_RetrieveAddedOrders()
    {
        //Arrange
        SellOrderRequest sellOrderRequest1 = new SellOrderRequest()
        {
            StockSymbol = "HDL",
            StockName = "Himalayan Distillery Limited",
            DateAndTimeOfOrder = DateTime.Parse("2000-01-01"),
            Quantity = 1,
            Price = 10000
        };
        SellOrderRequest sellOrderRequest2 = new SellOrderRequest()
        {
            StockSymbol = "CBBL",
            StockName = "Chhimek Bikas Bank Limited",
            DateAndTimeOfOrder = DateTime.Parse("2010-01-01"),
            Quantity = 100,
            Price = 9900
        };
        List<SellOrderRequest> sellOrderRequests = new List<SellOrderRequest>()
        {
            sellOrderRequest1, sellOrderRequest2
        };
        List<SellOrderResponse> sellOrderResponses = new List<SellOrderResponse>();
        foreach (SellOrderRequest sellOrderRequest in sellOrderRequests)
        {
            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);
            sellOrderResponses.Add(sellOrderResponse);
        }
        _testOutputHelper.WriteLine("Expected:");
        foreach (SellOrderResponse sellOrderResponse1 in sellOrderResponses)
        {
            _testOutputHelper.WriteLine(sellOrderResponse1.ToString());
        }

        //Act
        List<SellOrderResponse> sellOrderResponses1 = _stocksService.GetSellOrders();
        _testOutputHelper.WriteLine("Actual:");
        foreach (SellOrderResponse sellOrderResponse2 in sellOrderResponses1)
        {
            _testOutputHelper.WriteLine(sellOrderResponse2.ToString());
        }

        //Assert
        foreach (SellOrderResponse sellOrderResponse3 in sellOrderResponses)
        {
            Assert.Contains(sellOrderResponse3, sellOrderResponses1);
        }
    }
    #endregion
}