using MediatR;
using DateSurfer.Core.Domain.Enums;

namespace DateSurfer.Core.Application.Features.Membership.Commands;

public record CalculateMembershipFeeCommand(
    int UserId,
    MembershipType MembershipType
) : IRequest<decimal>;