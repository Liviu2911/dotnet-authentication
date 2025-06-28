using Microsoft.AspNetCore.Mvc;



    public class User
    {
        public Guid? id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
    }
