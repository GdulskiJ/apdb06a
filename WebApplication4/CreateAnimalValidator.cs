
using WebApplication3.DTOs;
using FluentValidation;
namespace WebApplication3;

public class CreateAnimalValidator : AbstractValidator<CreateAnimalRequest>

{
    public CreateAnimalValidator()
    {

        RuleFor(e => e.Name).MaximumLength(200);
        RuleFor(e => e.Descryption).MaximumLength(200).NotNull();
        RuleFor(e => e.Category).MaximumLength(200);
        RuleFor(e => e.Area).MaximumLength(200);
    }
    
    
    
}