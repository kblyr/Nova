namespace Nova;

public interface IMailOptions
{
    string Subject { get; }
    string Sender { get; }
}

public interface IMailOptions<TPayload> : IMailOptions { }