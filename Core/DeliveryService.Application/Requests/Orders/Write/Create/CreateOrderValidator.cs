using DeliveryService.Application.Enums;
using FluentValidation;

namespace DeliveryService.Application.Requests.Orders.Write.Create
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(request => request.Weight).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.Weight).GreaterThan(default(int)).WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.DistrictName).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.DistrictName).Must(DistrictNameValidation)
                .WithMessage("The delivery area of the order is incorrectly specified.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.DeliveryDateTime).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.DeliveryDateTime).Must(DeliveryTimeValidation)
                .WithMessage("Incorrect order delivery time.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
        }
        
        private bool DeliveryTimeValidation(DateTime orderTime) => orderTime >= DateTime.Now && orderTime <= DateTime.Now.AddDays(60);
        private bool DistrictNameValidation(string districtName)
        {
            District actualDistrict;
            bool result = Enum.TryParse(districtName, true, out actualDistrict);

            return result;
        }
    }
}
