public class PaymentServiceUnavailableException: Exception
{
    public PaymentServiceUnavailableException()
        : base("Payment service is currently unavailable.")
    {
    }
}