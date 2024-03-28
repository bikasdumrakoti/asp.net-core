using Entities;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.Enums;
using Services.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        //private field
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;

        //constructor
        public PersonsService(bool initialize = true)
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();

            if (initialize)
            {
                _persons.AddRange(new List<Person>() {
                new Person()
                {
                    PersonID = Guid.Parse("F4F7D2AA-CB2F-412A-AA8D-644D7CD390CD"),
                    PersonName = "Zacherie",
                    Email = "zblincko0@ameblo.jp",
                    DateOfBirth = DateTime.Parse("1996-05-09"),
                    Gender = "Male",
                    Address = "45 Golf Course Hill",
                    ReceiveNewsLetters = false,
                    CountryID=Guid.Parse("A004D3BF-B41F-45F4-98AF-A60C25180195")
                },
                new Person()
                {
                    PersonID = Guid.Parse("54D277E8-EB78-482C-8A3D-0A177170BAF7"),
                    PersonName = "Lynnette",
                    Email = "lwhawell1@apple.com",
                    DateOfBirth = DateTime.Parse("1992-07-05"),
                    Gender = "Female",
                    Address = "26 Badeau Avenue",
                    ReceiveNewsLetters = true,
                    CountryID=Guid.Parse("9682FDCA-00D9-4213-8481-D11A02BD617E")
                },
                new Person()
                {
                    PersonID = Guid.Parse("6F82BC51-74B9-451C-838B-6D64C8A968DA"),
                    PersonName = "Viviene",
                    Email = "vpauwel2@delicious.com",
                    DateOfBirth = DateTime.Parse("1999-05-31"),
                    Gender = "Female",
                    Address = "08186 Stuart Way",
                    ReceiveNewsLetters = true,
                    CountryID=Guid.Parse("9682FDCA-00D9-4213-8481-D11A02BD617E")
                },
                new Person()
                {
                    PersonID = Guid.Parse("FED7594B-6797-45B0-9D00-0B7FE367B6E4"),
                    PersonName = "Pierson",
                    Email = "penga3@drupal.org",
                    DateOfBirth = DateTime.Parse("1991-10-13"),
                    Gender = "Male",
                    Address = "41 Cambridge Terrace",
                    ReceiveNewsLetters = false,
                    CountryID=Guid.Parse("73894BD0-DA32-42B8-BD91-3C41A8547B50")
                },
                new Person()
                {
                    PersonID = Guid.Parse("1AFBE94D-0A85-409F-ADD9-390F15362EB2"),
                    PersonName = "Retha",
                    Email = "rjendas4@cornell.edu",
                    DateOfBirth = DateTime.Parse("1994-12-17"),
                    Gender = "Female",
                    Address = "48 Carberry Trail",
                    ReceiveNewsLetters = true,
                    CountryID=Guid.Parse("73894BD0-DA32-42B8-BD91-3C41A8547B50")
                },
                new Person()
                {
                    PersonID = Guid.Parse("5E176F9F-E048-4628-9F57-1BE103951684"),
                    PersonName = "Pen",
                    Email = "pburwin5@ca.gov",
                    DateOfBirth = DateTime.Parse("1991-07-20"),
                    Gender = "Female",
                    Address = "349 Scofield Terrace",
                    ReceiveNewsLetters = true,
                    CountryID=Guid.Parse("A86E0FB1-EF79-47E1-BED0-C001328CE50E")
                },
                new Person()
                {
                    PersonID = Guid.Parse("E4D5EA1B-90D4-4CD1-83C0-78AFCCDD8CC5"),
                    PersonName = "Anissa",
                    Email = "aoboyle6@4shared.com",
                    DateOfBirth = DateTime.Parse("1990-05-01"),
                    Gender = "Female",
                    Address = "7885 Butterfield Center",
                    ReceiveNewsLetters = true,
                    CountryID=Guid.Parse("7AC57FEC-FA82-4E8D-AB3A-563E587B24B0")
                },
                new Person()
                {
                    PersonID = Guid.Parse("1ACF2AF5-404C-4583-8341-9099BE023247"),
                    PersonName = "Alfie",
                    Email = "anormanvill7@toplist.cz",
                    DateOfBirth = DateTime.Parse("1997-12-21"),
                    Gender = "Male",
                    Address = "81856 Badeau Trail",
                    ReceiveNewsLetters = true,
                    CountryID=Guid.Parse("7AC57FEC-FA82-4E8D-AB3A-563E587B24B0")
                },
                new Person()
                {
                    PersonID = Guid.Parse("A6F251DE-5F2B-4405-862A-41851BF5DD4C"),
                    PersonName = "Haskel",
                    Email = "hbolwell8@google.pl",
                    DateOfBirth = DateTime.Parse("1999-04-09"),
                    Gender = "Male",
                    Address = "3006 Union Junction",
                    ReceiveNewsLetters = true,
                    CountryID=Guid.Parse("7AC57FEC-FA82-4E8D-AB3A-563E587B24B0")
                },
                new Person()
                {
                    PersonID = Guid.Parse("9C1A986A-B7DD-4D16-ABA1-998DB60A218F"),
                    PersonName = "Delmore",
                    Email = "dguslon9@github.io",
                    DateOfBirth = DateTime.Parse("1994-03-15"),
                    Gender = "Male",
                    Address = "01 Memorial Place",
                    ReceiveNewsLetters = true,
                    CountryID=Guid.Parse("7AC57FEC-FA82-4E8D-AB3A-563E587B24B0")
                }
                });
            }
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            //convert the Person object to PersonResponse type
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            //check if PersonAddRequest is not null
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            //Model validations
            ValidationHelper.ModelValidation(personAddRequest);

            //convert personAddRequest into Person type
            Person person = personAddRequest.ToPerson();

            //generate PersonID
            person.PersonID = Guid.NewGuid();

            //add person object to persons list
            _persons.Add(person);

            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(temp => ConvertPersonToPersonResponse(temp)).ToList();
        }

        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;

            Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);

            if (person == null)
                return null;

            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingPersons;

            switch (searchBy)
            {
                case nameof(PersonResponse.PersonName):
                    matchingPersons = allPersons.Where(temp => !string.IsNullOrEmpty(temp.PersonName) ? temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(PersonResponse.Email):
                    matchingPersons = allPersons.Where(temp => !string.IsNullOrEmpty(temp.Email) ? temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(PersonResponse.DateOfBirth):
                    matchingPersons = allPersons.Where(temp => temp.DateOfBirth != null ? temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(PersonResponse.Gender):
                    matchingPersons = allPersons.Where(temp => !string.IsNullOrEmpty(temp.Gender) ? temp.Gender.Equals(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(PersonResponse.CountryID):
                    matchingPersons = allPersons.Where(temp => !string.IsNullOrEmpty(temp.Country) ? temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(PersonResponse.Address):
                    matchingPersons = allPersons.Where(temp => !string.IsNullOrEmpty(temp.Address) ? temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                default:
                    matchingPersons = allPersons;
                    break;
            }
            return matchingPersons;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allPersons;

            List<PersonResponse> sortedPersons = (sortBy, sortOrder)
                switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                _ => allPersons
            };
            return sortedPersons;
        }

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(Person));

            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            //get matching person object to update
            Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonID == personUpdateRequest.PersonID);
            if (matchingPerson == null)
            {
                throw new ArgumentException("Given person id doesn't exist");
            }

            //update all details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            return ConvertPersonToPersonResponse(matchingPerson);
        }

        public bool DeletePerson(Guid? personID)
        {
            if (personID == null)
            {
                throw new ArgumentNullException(nameof(personID));
            }

            Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);
            if (person == null)
                return false;

            _persons.RemoveAll(temp => temp.PersonID == personID);

            return true;
        }
    }
}
