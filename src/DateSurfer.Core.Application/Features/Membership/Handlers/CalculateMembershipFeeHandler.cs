using DateSurfer.Core.Application.Features.Membership.Commands;
using DateSurfer.Core.Application.Services;
using DateSurfer.Core.Domain.Exceptions;
using DateSurfer.Core.Domain.Interfaces;
using MediatR;

namespace DateSurfer.Core.Application.Features.Membership.Handlers;

public class CalculateMembershipFeeHandler : IRequestHandler<CalculateMembershipFeeCommand, decimal>
{
    private readonly IMembershipFeeCalculator _feeCalculator;

    public CalculateMembershipFeeHandler(IMembershipFeeCalculator feeCalculator)
    {
        _feeCalculator = feeCalculator;
    }

    public async Task<decimal> Handle(CalculateMembershipFeeCommand request, CancellationToken cancellationToken)
    {
        return await _feeCalculator.CalculateFeeAsync(request.UserId, request.MembershipType);
    }
}