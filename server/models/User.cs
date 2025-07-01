using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;



public class User
{
    public Guid? id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}

public class GetUser
{
    public Guid? id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
}