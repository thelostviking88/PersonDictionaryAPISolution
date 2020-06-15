using API.Models;
using Data.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPersonService
    {
        //Task<IEnumerable<Person>> GetAll();
        Task<IEnumerable<PersonDto>> GetAll();
        Task<PersonDto> GetById(int id);
        Task<PersonDto> Save(PersonPostDto person);
        Task<PersonDto> Update(PersonPutDto person);
        Task<IEnumerable<PersonDto>> GetAllByCondition(string condition);
        Task<PersonDto> Delete(int id);
        Task<IEnumerable<ConnectionDto>> GetConnectionsByType(int id, string condition);
        Task<PersonDto> UpdateImage(int personId,string Url);
        Task<ConnectionDto> AddConnection(int id, ConnectionDto connectionDto);
        Task<PersonDto> DeleteConnection(int id, ConnectionDto connectionDto);
    }
}

