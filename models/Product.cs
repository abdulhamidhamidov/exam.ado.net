namespace shoping.models;

public class Product
{
    public int id { get; set; }
    public string name { get; set; }
    public string desc1 { get; set; }
    public string catigory_id { get; set; }
    public int price { get; set; }
    public DateTime created_at { get; set;}
}