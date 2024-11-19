using MediatR;

namespace PlanSphere.Core.Features.Users.Commands.ResetPassword;

public record ResetPasswordCommand(ulong UserId, string EmailVerificationToken, string ResetPasswordVerificationToken, string Password) : IRequest;
