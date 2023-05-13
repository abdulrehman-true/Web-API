using AutoMapper;
using Web_API.DTO.CharacterDTO;
using Web_API.DTO.SkillDTO;
using Web_API.DTO.WeaponDTO;
using Web_API.Models;

namespace Web_API
{
    public class AutoMapperProfile : Profile
    {
        //Tells c# what mapping to use and how mapping are being used Missing type map configuration or unsupported mapping.
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();

            CreateMap<AddCharacterDTO, Character>();
            CreateMap<Weapon, GetWeaponDTO>();
            CreateMap<Skill, GetSkillDTO>();
        }
    }
}