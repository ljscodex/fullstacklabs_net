namespace Lib.Repository.Entities;

public class Monster
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Hp { get; set; }
    public string? ImageUrl { get; set; }
    public int Speed { get; set; }
}