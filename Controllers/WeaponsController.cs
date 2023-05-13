using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API.DTO.CharacterDTO;
using Web_API.DTO.WeaponDTO;
using Web_API.Models;
using Web_API.Services;

namespace Web_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponsController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponsController(IWeaponService weaponService)
        {
            _weaponService = weaponService;

        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> AddWeapon(AddWeaponDTO addWeapon)
        {
            return Ok(await _weaponService.AddWeapon(addWeapon));
        }
    }
}