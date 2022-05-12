using MimeKit;

namespace Nova;

public interface IMailComposer<TPayload>
{
    Task<IResult> Compose(TPayload payload);
}

