using Web_API.DTO.CharacterDTO;
using Web_API.DTO.WeaponDTO;
using Web_API.Models;

namespace Web_API.Services
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDTO addWeapon);
         
    }
}