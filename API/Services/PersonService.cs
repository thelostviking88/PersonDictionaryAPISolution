using API.Helper;
using API.Models;
using AutoMapper;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class PersonService : IPersonService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PersonService(Repositories.Interfaces.IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = uow as UnitOfWork;
        }
        public async Task<IEnumerable<PersonDto>> GetAll()
        {
            var repResult = await _unitOfWork.PersonRepository.GetAllPersonsAsync();

            return _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(repResult);
        }

        public async Task<IEnumerable<PersonDto>> GetAllByCondition(string condition)
        {
            var repResult = await _unitOfWork.PersonRepository.GetByCondition(w => w.PrivateNumber.Contains(condition) || w.LastName.Contains(condition) || w.FirstName.Contains(condition)).ToListAsync();

            return _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(repResult);
        }

        public async Task<PersonDto> GetById(int id)
        {
            var repResult = await _unitOfWork.PersonRepository.GetPersonWithDetailsAsync(id);

            return _mapper.Map<Person, PersonDto>(repResult);
        }

        public async Task<PersonDto> Save(PersonPostDto person)
        {
            var personEntity = _mapper.Map<PersonPostDto, Person>(person);
            _unitOfWork.PersonRepository.CreatePerson(personEntity);
            await _unitOfWork.Commit();
            return _mapper.Map<Person, PersonDto>(personEntity);
        }
        public async Task<PersonDto> Update(PersonPutDto person)
        {
            var personEntity = await _unitOfWork.PersonRepository.GetPersonByIdAsync(person.Id);
            personEntity = _mapper.Map<PersonPutDto, Person>(person);
            _unitOfWork.PersonRepository.UpdatePerson(personEntity);
            await _unitOfWork.Commit();
            return _mapper.Map<Person, PersonDto>(personEntity);
        }

        public async Task<PersonDto> Delete(int id)
        {
            var personEntity = await _unitOfWork.PersonRepository.GetPersonWithDetailsAsync(id);
            _unitOfWork.PersonRepository.DeletePerson(personEntity);
            await _unitOfWork.Commit();
            return _mapper.Map<Person, PersonDto>(personEntity);
        }

        public async Task<ConnectionDto> AddConnection(int id, ConnectionDto connectionDto)
        {
            var connectionEntity = _mapper.Map<ConnectionDto, PersonConnection>(connectionDto);
            connectionEntity.PersonId = id;
            var personEntity = await _unitOfWork.PersonRepository.GetPersonWithDetailsAsync(id);
            if (personEntity == null)
            {
                throw new Exception();
            }
            _unitOfWork.ConnecttionRepository.CreateConnection(connectionEntity);
            await _unitOfWork.Commit();
            return _mapper.Map<PersonConnection, ConnectionDto>(connectionEntity);
        }

        public async Task<PersonDto> DeleteConnection(int id, ConnectionDto connectionDto)
        {
            var personEntity = await _unitOfWork.PersonRepository.GetPersonWithDetailsAsync(id);
            if (personEntity == null)
            {
                throw new Exception();
            }
            var connectionEntity = await _unitOfWork.ConnecttionRepository.GetConnectionByConnectedPersonId(connectionDto.ConnectedPersonId);
            _unitOfWork.ConnecttionRepository.Delete(connectionEntity);
            await _unitOfWork.Commit();

            return _mapper.Map<Person, PersonDto>(personEntity);
        }

        public async Task<IEnumerable<ConnectionDto>> GetConnectionsByType(int id, string condition)
        {
            var repResult = await _unitOfWork.PersonRepository.GetPersonWithDetailsAsync(id);
            var connections = repResult.PersonConnectionPerson.Where(w => w.ConnectionType.ToLower().Equals(condition.ToLower()));

            return _mapper.Map<IEnumerable<PersonConnection>, IEnumerable<ConnectionDto>>(connections);
        }

        public async Task<PersonDto> UpdateImage(int personId, string imageUrl)
        {
            try
            {
                var personEntity = await _unitOfWork.PersonRepository.GetPersonByIdAsync(personId);
                var ImageOld = personEntity.Picture;
                personEntity.Picture = imageUrl;
                _unitOfWork.PersonRepository.UpdatePerson(personEntity);
                await _unitOfWork.Commit();
                if (!string.IsNullOrWhiteSpace(ImageOld))
                {
                    ImageHelper.Remove(ImageOld);
                }
                return _mapper.Map<Person, PersonDto>(personEntity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
