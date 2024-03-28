using Entities;
using ServiceContracts;
using ServiceContracts.DTOs;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //private field
        private readonly List<Country> _countries;

        //constructor
        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();
            if (initialize)
            {
                _countries.AddRange(new List<Country>() {
                new Country()
                {
                    CountryId=Guid.Parse("A004D3BF-B41F-45F4-98AF-A60C25180195"),
                    CountryName="USA"
                },
                new Country()
                {
                    CountryId = Guid.Parse("9682FDCA-00D9-4213-8481-D11A02BD617E"),
                    CountryName = "Canada"
                },
                new Country()
                {
                    CountryId = Guid.Parse("73894BD0-DA32-42B8-BD91-3C41A8547B50"),
                    CountryName = "UK"
                },
                new Country()
                {
                    CountryId = Guid.Parse("A86E0FB1-EF79-47E1-BED0-C001328CE50E"),
                    CountryName = "India"
                },
                new Country()
                {
                    CountryId = Guid.Parse("7AC57FEC-FA82-4E8D-AB3A-563E587B24B0"),
                    CountryName = "Australia"
                }
                });
            }
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            //Validation: countryAddRequest parameter can't be null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            //Validation: CountryName can't be null
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            //Validation: CountryName can't be duplicate
            if (_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }

            //Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            //generate CountryID
            country.CountryId = Guid.NewGuid();

            //Add country object into _countries
            _countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
                return null;

            Country? country_response_from_list = _countries.FirstOrDefault(temp => temp.CountryId == countryID);

            if (country_response_from_list == null)
                return null;

            return country_response_from_list.ToCountryResponse();
        }
    }
}