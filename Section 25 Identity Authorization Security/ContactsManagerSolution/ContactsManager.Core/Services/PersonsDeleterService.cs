using Entities;
using ServiceContracts;
using RepositoryContracts;

namespace Services
{
    public class PersonsDeleterService : IPersonsDeleterService
    {
        //private field
        private readonly IPersonsRepository _personsRepository;

        //constructor
        public PersonsDeleterService(IPersonsRepository personsRepository)
        {
            _personsRepository = personsRepository;
        }

        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null)
            {
                throw new ArgumentNullException(nameof(personID));
            }

            Person? person = await _personsRepository.GetPersonByPersonID(personID.Value);
            if (person == null)
                return false;

            await _personsRepository.DeletePersonByPersonID(personID.Value);

            return true;
        }
    }
}
