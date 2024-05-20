using ServiceContracts;
using Entities;
using ServiceContracts.DTOs;
using Moq;
using RepositoryContracts;
using AutoFixture;
using FluentAssertions;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesGetterService _countriesGetterService;
        private readonly ICountriesAdderService _countriesAdderService;

        private readonly Mock<ICountriesRepository> _countriesRepositoryMock;
        private readonly ICountriesRepository _countriesRepository;

        private readonly IFixture _fixture;

        public CountriesServiceTest()
        {
            _fixture = new Fixture();

            _countriesRepositoryMock = new Mock<ICountriesRepository>();
            _countriesRepository = _countriesRepositoryMock.Object;

            _countriesGetterService = new CountriesGetterService(_countriesRepository);
            _countriesAdderService = new CountriesAdderService(_countriesRepository);
        }

        #region AddCountry
        //When the CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddCountry_NullCountry_ToBeArgumentNullException()
        {
            //Arrange
            CountryAddRequest? countryAddRequest = null;

            //Act
            Func<Task> action = async () =>
            {
                await _countriesAdderService.AddCountry(countryAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //When the CountryName is null, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_CountryNameIsNull_ToBeArgumentException()
        {
            //Arrange
            CountryAddRequest? countryAddRequest = _fixture.Build<CountryAddRequest>()
                .With(temp => temp.CountryName, null as string)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _countriesAdderService.AddCountry(countryAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //When the CountryName is duplicate, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_DuplicateCountryName_ToBeArgumentException()
        {
            //Arrange
            CountryAddRequest first_country_request = _fixture.Build<CountryAddRequest>()
                 .With(temp => temp.CountryName, "Test name").Create();
            CountryAddRequest second_country_request = _fixture.Build<CountryAddRequest>()
              .With(temp => temp.CountryName, "Test name").Create();

            Country first_country = first_country_request.ToCountry();
            Country second_country = second_country_request.ToCountry();

            _countriesRepositoryMock
             .Setup(temp => temp.AddCountry(It.IsAny<Country>()))
             .ReturnsAsync(first_country);

            //Return null when GetCountryByCountryName is called
            _countriesRepositoryMock
             .Setup(temp => temp.GetCountryByCountryName(It.IsAny<string>()))
             .ReturnsAsync(null as Country);

            CountryResponse first_country_from_add_country = await _countriesAdderService.AddCountry(first_country_request);

            //Act
            var action = async () =>
            {
                //Return first country when GetCountryByCountryName is called
                _countriesRepositoryMock.Setup(temp => temp.AddCountry(It.IsAny<Country>())).ReturnsAsync(first_country);

                _countriesRepositoryMock.Setup(temp => temp.GetCountryByCountryName(It.IsAny<string>())).ReturnsAsync(first_country);

                await _countriesAdderService.AddCountry(second_country_request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //When you supply proper CountryName, it should insert (add) the country to the existing list of countries
        [Fact]
        public async Task AddCountry_ProperCountryDetails_ToBeSuccessful()
        {
            //Arrange
            CountryAddRequest? countryAddRequest = _fixture.Create<CountryAddRequest>();

            Country country = countryAddRequest.ToCountry();
            CountryResponse country_response_expected = country.ToCountryResponse();

            _countriesRepositoryMock
                .Setup(temp => temp.AddCountry(It.IsAny<Country>()))
                .ReturnsAsync(country);

            //Act
            CountryResponse countryResponse = await _countriesAdderService.AddCountry(countryAddRequest);
            country_response_expected.CountryID = countryResponse.CountryID;

            //Assert
            countryResponse.CountryID.Should().NotBe(Guid.Empty);
            countryResponse.Should().Be(country_response_expected);
        }
        #endregion

        #region GetAllCountries
        //The list of countries should be empty by default (before adding any countries)
        [Fact]
        public async Task GetAllCountries_EmptyList()
        {
            //Arrange
            var countries = new List<Country>();
            _countriesRepositoryMock
                .Setup(temp => temp.GetAllCountries())
                .ReturnsAsync(countries);

            //Act
            List<CountryResponse> actual_country_response_list = await _countriesGetterService.GetAllCountries();

            //Assert
            actual_country_response_list.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllCountries_AddFewCountries_ToBeSuccessful()
        {
            List<Country> countries = new List<Country>()
            {
                _fixture
                .Build<Country>()
                .With(temp=>temp.Persons, null as List<Person>)
                .Create(),

                _fixture
                .Build<Country>()
                .With(temp=>temp.Persons, null as List<Person>)
                .Create()
            };

            List<CountryResponse> expected_country_response = countries.Select(temp => temp.ToCountryResponse()).ToList();

            _countriesRepositoryMock
                .Setup(temp => temp.GetAllCountries())
                .ReturnsAsync(countries);

            //Act
            List<CountryResponse> actualCountryResponseList = await _countriesGetterService.GetAllCountries();

            //read each element from countries_list_from_add_country
            actualCountryResponseList.Should().BeEquivalentTo(expected_country_response);
        }
        #endregion

        #region GetCountryByCountryID
        //If we supply null as CountryID, it should return null as CountryResponse
        [Fact]
        public async Task GetCountryByCountryID_NullCountryID_ToBeNull()
        {
            //Arrange
            Guid? countryID = null;

            //Act
            CountryResponse? country_response_from_get_method = await _countriesGetterService.GetCountryByCountryID(countryID);

            //Assert
            country_response_from_get_method.Should().BeNull();
        }

        //If we supply a valid country id, it should return the matching country details as CountryResponse object
        [Fact]
        public async Task GetCountryByCountryID_ValidCountryID()
        {
            //Arrange
            Country country = _fixture.Build<Country>()
              .With(temp => temp.Persons, null as List<Person>)
              .Create();
            CountryResponse country_response = country.ToCountryResponse();

            _countriesRepositoryMock
             .Setup(temp => temp.GetCountryByCountryID(It.IsAny<Guid>()))
             .ReturnsAsync(country);

            //Act
            CountryResponse? country_response_from_get = await _countriesGetterService.GetCountryByCountryID(country.CountryID);

            //Assert
            country_response_from_get.Should().Be(country_response);
        }
        #endregion
    }
}
