using Domain.Entities;
using FluentValidation;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommandValidator:AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
    }
}