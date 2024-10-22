using DeliveryService.Application.Enums;
using DeliveryService.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Application.Requests.Orders.Write.Delete
{
    public class DeleteOrderValidator : AbstractValidator<DeleteOrderRequest>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOrderValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Id).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Id).MustAsync(OrderExists)
                .WithMessage("There is no such order")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> OrderExists(Guid id, CancellationToken cancellation) =>
            await _context.Order.AnyAsync(x => x.Id == id, cancellation);
    }
}
