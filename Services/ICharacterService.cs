using Web_API.DTO.CharacterDTO;
using Web_API.Models;

namespace Web_API.Services
{
    public interface ICharacterService
    {
         Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters();
         Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO character);
         Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id);
         Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter);
         Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id);
         Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill);
          
    }
}