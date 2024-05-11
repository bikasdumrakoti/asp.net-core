using AutoFixture;
using Entities;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTOs;
using Services;
using Xunit.Abstractions;

namespace UnitTests
{
    public class StocksServiceTest
    {
        private readonly Mock<IStocksRepository> _stocksRepositoryMock;
        private readonly IStocksRepository _stocksRepository;
        private readonly IStocksService _stocksService;

        private readonly IFixture _fixture;

        private readonly ITestOutputHelper _testOutputHelper;

        public StocksServiceTest(ITestOutputHelper testOutputHelper)
        {
            _stocksRepositoryMock = new Mock<IStocksRepository>();
            _stocksRepository = _stocksRepositoryMock.Object;
            _stocksService = new StocksService(_stocksRepository);

            _fixture = new Fixture();

            _testOutputHelper = testOutputHelper;
        }

        #region CreateBuyOrder
        [Fact]
        public async Task CreateBuyOrder_NullRequest_ToBeArgumentNullException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = null;

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateBuyOrder_QuantityIsLessThanRange_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, 0)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_QuantityIsGreaterThanRange_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, 100001)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_PriceIsLessThanRange_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Price, 0)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_PriceIsGreaterThanRange_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Price, 10001)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_NullStockSymbol_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_DateAndTimeOfOrderIsGreater_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("1999-12-31"))
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_ProperBuyOrder_ToBeSuccessful()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2000-01-01"))
                .With(temp => temp.Quantity, 1)
                .With(temp => temp.Price, 10000)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderID = Guid.NewGuid();
            BuyOrderResponse expectedBuyOrderResponse = buyOrder.ToBuyOrderResponse();

            _stocksRepositoryMock
                .Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

            //Act
            BuyOrderResponse actualBuyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            //Assert
            actualBuyOrderResponse.BuyOrderID.Should().NotBe(Guid.Empty);
            actualBuyOrderResponse.Should().Be(expectedBuyOrderResponse);
        }
        #endregion

        #region CreateSellOrder
        [Fact]
        public async Task CreateSellOrder_NullRequest_ToBeArgumentNullException()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = null;

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateSellOrder_QuantityIsLessThanRange_ToBeArgumentException()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, 0)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_QuantityIsGreaterThanRange_ToBeArgumentException()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, 100001)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_PriceIsLessThanRange_ToBeArgumentException()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Price, 0)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_PriceIsGreaterThanRange_ToBeArgumentException()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Price, 10001)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_NullStockSymbol_ToBeArgumentException()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_DateAndTimeOfOrderIsGreater_ToBeArgumentException()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("1999-12-31"))
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_ProperBuyOrder_ToBeSuccessful()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2000-01-01"))
                .With(temp => temp.Quantity, 1)
                .With(temp => temp.Price, 10000)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderID = Guid.NewGuid();
            SellOrderResponse expectedSellOrderResponse = sellOrder.ToSellOrderResponse();

            _stocksRepositoryMock
                .Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            //Act
            SellOrderResponse actualSellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

            //Assert
            actualSellOrderResponse.SellOrderID.Should().NotBe(Guid.Empty);
            actualSellOrderResponse.Should().Be(expectedSellOrderResponse);
        }
        #endregion

        #region GetBuyOrders
        [Fact]
        public async Task GetBuyOrders_EmptyList()
        {
            //Arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>();

            _stocksRepositoryMock
                .Setup(temp => temp.GetBuyOrders())
                .ReturnsAsync(buyOrders);

            //Act
            List<BuyOrderResponse> actualBuyOrderResponses = await _stocksService.GetBuyOrders();

            //Assert
            actualBuyOrderResponses.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBuyOrders_WithFewBuyOrders_ToBeSuccessful()
        {
            //Arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>()
            {
                _fixture.Build<BuyOrder>()
                .With(temp=>temp.DateAndTimeOfOrder, DateTime.Parse("2000-01-01"))
                .With(temp=>temp.Quantity, 1)
                .With(temp=>temp.Price, 10000)
                .Create(),

                _fixture.Build<BuyOrder>()
                .With(temp=>temp.DateAndTimeOfOrder, DateTime.Parse("2010-01-01"))
                .With(temp=>temp.Quantity, 100)
                .With(temp=>temp.Price, 9900)
                .Create(),
            };

            List<BuyOrderResponse> expectedBuyOrderResponses = buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (BuyOrderResponse expectedBuyOrderResponse in expectedBuyOrderResponses)
            {
                _testOutputHelper.WriteLine(expectedBuyOrderResponse.ToString());
            }

            _stocksRepositoryMock
                .Setup(temp => temp.GetBuyOrders())
                .ReturnsAsync(buyOrders);

            //Act
            List<BuyOrderResponse> actualBuyOrderResponses = await _stocksService.GetBuyOrders();
            _testOutputHelper.WriteLine("Actual:");
            foreach (BuyOrderResponse actualBuyOrderResponse in actualBuyOrderResponses)
            {
                _testOutputHelper.WriteLine(actualBuyOrderResponse.ToString());
            }

            //Assert
            actualBuyOrderResponses.Should().BeEquivalentTo(expectedBuyOrderResponses);
        }
        #endregion

        #region GetSellOrders
        [Fact]
        public async Task GetSellOrders_EmptyList()
        {
            //Arrange
            List<SellOrder> sellOrders = new List<SellOrder>();

            _stocksRepositoryMock
                .Setup(temp => temp.GetSellOrders())
                .ReturnsAsync(sellOrders);

            //Act
            List<SellOrderResponse> actualSellOrderResponses = await _stocksService.GetSellOrders();

            //Assert
            actualSellOrderResponses.Should().BeEmpty();
        }

        [Fact]
        public async Task GetSellOrders_WithFewSellOrders_ToBeSuccessful()
        {
            //Arrange
            List<SellOrder> sellOrders = new List<SellOrder>()
            {
                _fixture.Build<SellOrder>()
                .With(temp=>temp.DateAndTimeOfOrder, DateTime.Parse("2000-01-01"))
                .With(temp=>temp.Quantity, 1)
                .With(temp=>temp.Price, 10000)
                .Create(),

                _fixture.Build<SellOrder>()
                .With(temp=>temp.DateAndTimeOfOrder, DateTime.Parse("2010-01-01"))
                .With(temp=>temp.Quantity, 100)
                .With(temp=>temp.Price, 9900)
                .Create(),
            };

            List<SellOrderResponse> expectedSellOrderResponses = sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (SellOrderResponse expectedSellOrderResponse in expectedSellOrderResponses)
            {
                _testOutputHelper.WriteLine(expectedSellOrderResponse.ToString());
            }

            _stocksRepositoryMock
                .Setup(temp => temp.GetSellOrders())
                .ReturnsAsync(sellOrders);

            //Act
            List<SellOrderResponse> actualSellOrderResponses = await _stocksService.GetSellOrders();
            _testOutputHelper.WriteLine("Actual:");
            foreach (SellOrderResponse actualSellOrderResponse in actualSellOrderResponses)
            {
                _testOutputHelper.WriteLine(actualSellOrderResponse.ToString());
            }

            //Assert
            actualSellOrderResponses.Should().BeEquivalentTo(expectedSellOrderResponses);
        }
        #endregion
    }
}