using System.Security.Claims;
using AutoMapper;
using Web_API.DTO.CharacterDTO;
using Web_API.Models;

namespace Web_API.Services
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> ch = new List<Character>
        {
            new Character(),
            new Character{Name = "Abdul"}
        };
        private readonly IMapper _mapper;
        private readonly DataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO character)
        {
            var response = new ServiceResponse<List<GetCharacterDTO>>();
            var characterToAdd = _mapper.Map<Character>(character);
            characterToAdd.User = await _dbContext.Users.FirstOrDefaultAsync(u=> u.Id == GetUserId());
            _dbContext.Characters.Add(characterToAdd);
            await _dbContext.SaveChangesAsync();
            response.Data = await _dbContext.Characters
            .Where(u=> u.User!.Id == GetUserId())
            .Select(c => _mapper.Map<GetCharacterDTO>(c))
            .ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDTO>>();
            try
            {
                var characterToDelete = await _dbContext.Characters
                    .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
                if (characterToDelete is null)
                    throw new Exception($"Character with {id} not found");
                _dbContext.Characters.Remove(characterToDelete);
                await _dbContext.SaveChangesAsync();

                response.Data = await _dbContext.Characters
                    .Where(c=> c.User!.Id ==GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        private int GetUserId()=> int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);


        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacters = await _dbContext.Characters
                .Include(c=> c.Skills)
                .Include(c=> c.Weapon)
                .Where(c => c.User!.Id == GetUserId()).ToListAsync();
            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            var dbChtoFind = await _dbContext.Characters
                .Include(c=> c.Weapon)
                .Include(c=> c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
            response.Data = _mapper.Map<GetCharacterDTO>(dbChtoFind);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            try
            {
                var characterToUpdate = await _dbContext.Characters
                    .Include(c=> c.User)//We tell ef core to include user object into character to make relationship explicitly so that user is not null
                    .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (characterToUpdate is null || characterToUpdate.User!.Id != GetUserId())
                    throw new Exception($"Character with {updatedCharacter.Id} not found");
                characterToUpdate.Name = updatedCharacter.Name;
                characterToUpdate.Defence = updatedCharacter.Defence;
                characterToUpdate.HitPoints = updatedCharacter.Defence;
                characterToUpdate.Intelligence = updatedCharacter.Intelligence;
                characterToUpdate.rpg = updatedCharacter.rpg;
                characterToUpdate.Strength = updatedCharacter.Strength;

                await _dbContext.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDTO>(characterToUpdate);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            try
            {
                var character = await _dbContext.Characters
                    .Include(c=> c.Weapon)
                    .Include(c=> c.Skills) // use then after include to get sub data in the skills or any other object
                    .FirstOrDefaultAsync(c=> c.Id == newCharacterSkill.CharacterId &&
                        c.User!.Id == GetUserId());
                
                if(character is null)
                {
                    response.Message = "Character not found";
                    response.Success = false;
                    return response;
                }

                var skill = await _dbContext.Skills.FirstOrDefaultAsync(s=> s.Id == newCharacterSkill.SkillId);

                if(skill is null)
                {
                    response.Message = "Skill not found";
                    response.Success = false;
                    return response;
                }
                character.Skills!.Add(skill);
                await _dbContext.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDTO>(character);

            }
            catch(Exception ex)
            {

            }
            return response;
        }
    }
}