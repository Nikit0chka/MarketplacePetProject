namespace Entities.DataBase.Models;

public class Administrator
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? Patronymic { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required AdministratorPosition Position { get; set; }
}