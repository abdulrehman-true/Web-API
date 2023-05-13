using Web_API.Models;

namespace Web_API.DTO.CharacterDTO
{
    public class AddCharacterDTO
    {
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        
        public RpgClass rpg { get; set; } = RpgClass.Knight;
    }
}