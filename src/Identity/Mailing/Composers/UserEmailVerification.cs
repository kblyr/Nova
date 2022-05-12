namespace Nova.Identity.Composers;

sealed class UserEmailVerificationComposer : IMailComposer<IUserEmailVerificationPayload>
{
    readonly IMailTemplateLoader<IUserEmailVerificationPayload> _templateLoader;
    readonly IMailOptions<IUserEmailVerificationPayload> _options;

    public UserEmailVerificationComposer(IMailTemplateLoader<IUserEmailVerificationPayload> templateLoader, IMailOptions<IUserEmailVerificationPayload> options)
    {
        _templateLoader = templateLoader;
        _options = options;
    }

    public async Task<IResult> Compose(IUserEmailVerificationPayload payload)
    {
        if (payload is not IUserEmailVerificationPayloadWithToken model)
            throw UnsupportedInstanceException.CreateInstance<IUserEmailVerificationPayloadWithToken>(payload?.GetType());

        var templateLoaderResult = await _templateLoader.Load();

        if (templateLoaderResult is not MailTemplateLoadedResult templateLoadedResult)
            return templateLoaderResult;

        var output = await templateLoadedResult.Template.RenderAsync(model);
        var message = new MimeMessage();
        message.Sender = MailboxAddress.Parse(_options.Sender);
        message.To.Add(MailboxAddress.Parse(model.EmailAddress));
        message.Subject = _options.Subject;
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = output;
        message.Body = bodyBuilder.ToMessageBody();
        return new MimeMessageComposedResult(message);
    }
}
