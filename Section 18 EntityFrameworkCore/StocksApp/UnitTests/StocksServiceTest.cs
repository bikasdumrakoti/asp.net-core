using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTOs;
using Services;
using Xunit.Abstractions;

namespace UnitTests
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;
        private readonly ITestOutputHelper _testOutputHelper;

        public StocksServiceTest(ITestOutputHelper testOutputHelper)
        {
            _stocksService = new StocksService(new StockMarketDbContext(new DbContextOptionsBuilder<StockMarketDbContext>().Options));
            _testOutputHelper = testOutputHelper;
        }

        #region CreateBuyOrder
        [Fact]
        public async Task CreateBuyOrder_RequestIsNull()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_QuantityIsLessThanRange()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "HDL",
                StockName = "Himalayan Distillery Limited",
                Quantity = 0
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_QuantityIsGreaterThanRange()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "HDL",
                StockName = "Himalayan Distillery Limited",
                Quantity = 100001
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_PriceIsLessThanRange()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "HDL",
                StockName = "Himalayan Distillery Limited",
                Price = 0
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_PriceIsGreaterThanRange()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "HDL",
                StockName = "Himalayan Distillery Limited",
                Price = 10001
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_StockSymbolIsNull()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = null,
                StockName = "Himalayan Distillery Limited"
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_DateAndTimeOfOrderIsGreater()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "HDL",
                StockName = "Himalayan Distillery Limited",
                DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_ValidBuyOrder()
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
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();

            //Assert
            Assert.True(buyOrderResponse.BuyOrderID != Guid.Empty);
            Assert.Contains(buyOrderResponse, buyOrderResponses);
        }
        #endregion

        #region CreateSellOrder
        [Fact]
        public async Task CreateSellOrder_RequestIsNull()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_QuantityIsLessThanRange()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "HDL",
                StockName = "Himalayan Distillery Limited",
                Quantity = 0
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_QuantityIsGreaterThanRange()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "HDL",
                StockName = "Himalayan Distillery Limited",
                Quantity = 100001
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_PriceIsLessThanRange()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "HDL",
                StockName = "Himalayan Distillery Limited",
                Price = 0
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_PriceIsGreaterThanRange()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "HDL",
                StockName = "Himalayan Distillery Limited",
                Price = 10001
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_StockSymbolIsNull()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = null,
                StockName = "Himalayan Distillery Limited"
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_DateAndTimeOfOrderIsGreater()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "HDL",
                StockName = "Himalayan Distillery Limited",
                DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_ValidSellOrder()
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
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();

            //Assert
            Assert.True(sellOrderResponse.SellOrderID != Guid.Empty);
            Assert.Contains(sellOrderResponse, sellOrderResponses);
        }
        #endregion

        #region GetBuyOrders
        [Fact]
        public async Task GetBuyOrders_EmptyList()
        {
            //Act
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();

            //Assert
            Assert.Empty(buyOrderResponses);
        }

        [Fact]
        public async Task GetBuyOrders_RetrieveAddedOrders()
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
                BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
                buyOrderResponses.Add(buyOrderResponse);
            }
            _testOutputHelper.WriteLine("Expected:");
            foreach (BuyOrderResponse buyOrderResponse1 in buyOrderResponses)
            {
                _testOutputHelper.WriteLine(buyOrderResponse1.ToString());
            }

            //Act
            List<BuyOrderResponse> buyOrderResponses1 = await _stocksService.GetBuyOrders();
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
        public async Task GetSellOrders_EmptyList()
        {
            //Act
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();

            //Assert
            Assert.Empty(sellOrderResponses);
        }

        [Fact]
        public async Task GetSellOrders_RetrieveAddedOrders()
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
                SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);
                sellOrderResponses.Add(sellOrderResponse);
            }
            _testOutputHelper.WriteLine("Expected:");
            foreach (SellOrderResponse sellOrderResponse1 in sellOrderResponses)
            {
                _testOutputHelper.WriteLine(sellOrderResponse1.ToString());
            }

            //Act
            List<SellOrderResponse> sellOrderResponses1 = await _stocksService.GetSellOrders();
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
}