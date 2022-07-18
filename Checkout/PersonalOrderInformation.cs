public class PersonalOrderInformation
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? ShippingAddress { get; set; }
    public long? CreditCardNumber { get; set; }
    public int? CreditCardExperationMonth { get; set; }
    public int? CreditCardExperationYear { get; set; }
    public int? CreditCardCVV { get; set; }
}