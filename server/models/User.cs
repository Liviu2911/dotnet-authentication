using Microsoft.AspNetCore.Mvc;



    public class User(Guid? Id, string Username, string Email, string Password)
    {
    public Guid? id { get; set; } = Id;
    public string? username { get; set; } = Username;
    public string? password { get; set; } = Password;
    public string? email { get; set; } = Email;
    }
