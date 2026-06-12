public class CreatePaymentRequestDto
{
    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }
}