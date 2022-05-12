using MimeKit;
using Org.BouncyCastle.Math.Field;

namespace Nova.Results;

public record MimeMessageSentResult(MimeMessage Message) : IResult;