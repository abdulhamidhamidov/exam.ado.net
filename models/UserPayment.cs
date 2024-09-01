namespace shoping.models;

public class UserPayment
{
    public int id { get; set;  }
    public int user_id { get; set; }
    public string payment_type { get; set; }
    public string provider { get; set; }
    public int account_no { get; set; }
    public DateTime expiry { get; set; }
}