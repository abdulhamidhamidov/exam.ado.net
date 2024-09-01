namespace shoping.models;

public class User
{
    public int Id { get; set; }
    public int Age { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; }= null!;
    public string Email { get; set; }= null!;
}