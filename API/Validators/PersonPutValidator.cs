using API.Models;
using API.Validators;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Localization;
using Serilog;
using System;

namespace API.Filters
{
    public class PersonPutValidator : AbstractValidator<PersonPutDto>
    {

        public PersonPutValidator(IStringLocalizer<PersonPutValidator> localizer)
        {
            ValidatorOptions.LanguageManager.Culture = System.Threading.Thread.CurrentThread.CurrentCulture;

            RuleFor(x => x.Id).GreaterThan(0).WithMessage(x => localizer["IdIsRequired"]).OnFailure(OnError, localizer["IdIsRequired"]);

            RuleFor(x => x.FirstName).Cascade(CascadeMode.StopOnFirstFailure).
                NotEmpty().WithMessage(x => localizer["NameIsRequired"]).
                Matches(@"^[a-zA-Z]{2,50}$|^[ა-ჰ]{2,50}$")
                .WithMessage(x => localizer["NameIs2-50LatinOrGeorgian"]).OnFailure(OnError, localizer["NameIs2-50LatinOrGeorgian"]);

            RuleFor(x => x.LastName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(x => localizer["LastNameIsRequired"]).
                Matches(@"^[a-zA-Z]{2,50}$|^[ა-ჰ]{2,50}$")
                .WithMessage(x => localizer["LastNameIs2-50LatinOrGeorgian"]).OnFailure(OnError, localizer["LastNameIs2-50LatinOrGeorgian"]);

            RuleFor(x => x.Gender).Matches(@"^(ქალი|კაცი)$").WithMessage(x => localizer["GenderValues"]).OnFailure(OnError, localizer["GenderValues"]);

            RuleFor(x => x.PrivateNumber).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(x => localizer["PersonalIDIsRequired"])
                .Matches(@"^(\d){11}$").WithMessage(x => localizer["PersonalIDRule"]).OnFailure(OnError, localizer["PersonalIDRule"]);

            RuleFor(x => x.DateOfBirth).Must(birth => birth < DateTime.Now.AddYears(-18))
                .WithMessage(x => localizer["DateOfBirthMin18Years"]).OnFailure(OnError, localizer["DateOfBirthMin18Years"]);

            RuleFor(x => x.CityId).NotEqual(0).WithMessage(x => localizer["CityIsRequired"]).OnFailure(OnError, localizer["CityIsRequired"]);


        }
        private void OnError(PersonPutDto obj, PropertyValidatorContext ctx, string message)
        {
            Log.Error("error: {0} - {1} :- {2}", ctx.PropertyName, ctx.PropertyValue, message);
        }
    }
}
