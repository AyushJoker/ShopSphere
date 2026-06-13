public class CreatePaymentRequestDto
{
    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public bool SimulateFailure { get; set; }
}