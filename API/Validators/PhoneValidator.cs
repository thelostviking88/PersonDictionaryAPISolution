using API.Models;
using API.Validators;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Localization;
using Serilog;

namespace API.Filters
{
    public class PhoneValidator : AbstractValidator<PhoneDto>
    {

        public PhoneValidator(IStringLocalizer<PhoneValidator> localizer)
        {
            ValidatorOptions.LanguageManager.Culture = System.Threading.Thread.CurrentThread.CurrentCulture;

            RuleFor(x => x.Number).Cascade(CascadeMode.StopOnFirstFailure).
                Length(4, 50).WithMessage(x => localizer["PhoneLength"]).OnFailure(OnError, localizer["CityIsRequired"]);

            RuleFor(x => x.Type).Matches(@"^(მობილური|ოფისის|სახლის)$").WithMessage(x => localizer["PhoneTypes"]).OnFailure(OnError, localizer["CityIsRequired"]);
        }

        private void OnError(PhoneDto obj, PropertyValidatorContext ctx, string message)
        {
            Log.Error("error: {0} - {1} :- {2}", ctx.PropertyName, ctx.PropertyValue, message);
        }
    }
}
