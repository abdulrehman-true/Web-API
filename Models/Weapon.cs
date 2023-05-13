namespace Web_API.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public Character? Character { get; set; }
        public int CharacterId { get; set; }// EF knows that this is a foreign key because class ka name hai agay id hai. Khud hi relation bana de ga.

    }
}