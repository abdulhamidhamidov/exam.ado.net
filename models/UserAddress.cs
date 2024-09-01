namespace shoping.models;

public class UserAddress
{
    public int id { get; set; } 
    public int user_id { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string postal_code { get; set; }
    public string country { get; set; }
    public string telephone { get; set; }
}