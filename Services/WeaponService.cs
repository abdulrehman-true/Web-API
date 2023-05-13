using System.Security.Claims;
using AutoMapper;
using Web_API.DTO.CharacterDTO;
using Web_API.DTO.WeaponDTO;
using Web_API.Models;

namespace Web_API.Services
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public WeaponService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.dataContext = dataContext;

        }
        public async Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDTO addWeapon)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            try
            {
                var character = await dataContext.Characters
                    .FirstOrDefaultAsync(c=> c.Id == addWeapon.CharacterId &&
                    c.User!.Id == int.Parse(httpContextAccessor.HttpContext!.User
                        .FindFirstValue(ClaimTypes.NameIdentifier)!));

                if(character is null)
                {
                    response.Message = "Character not found";
                    response.Success = false;
                    return response;
                }      

            var weapon = new Weapon
            {
                Name = addWeapon.Name,
                Damage = addWeapon.Damage,
                Character = character
            };//Manual mapping just like auto mapper
            dataContext.Add(weapon);
            await dataContext.SaveChangesAsync();
            response.Data = mapper.Map<GetCharacterDTO>(character);

            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;

            }
            return response;
        }
    }
}