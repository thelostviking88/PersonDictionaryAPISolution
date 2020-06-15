using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Linq;

namespace API.Validators
{
    public static  class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> OnFailure<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Action<T, PropertyValidatorContext> action)
        {
            return rule.Configure(cfg =>
            {
                cfg.ReplaceValidator(cfg.CurrentValidator, new OnFailureValidator<T, TProperty>(cfg.CurrentValidator, action));
            });
        }

        public static IRuleBuilderOptions<T, TProperty> OnFailure<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Action<T, PropertyValidatorContext, string> action, string message)
        {
            return rule.OnFailure(action, message, null as object[]);
        }

        public static IRuleBuilderOptions<T, TProperty> OnFailure<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Action<T, PropertyValidatorContext, string> action, string message, params object[] messageArgs)
        {
            var funcs = ConvertArrayOfObjectsToArrayOfDelegates<T>(messageArgs);
            return rule.OnFailure(action, message, funcs);
        }

        public static IRuleBuilderOptions<T, TProperty> OnFailure<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Action<T, PropertyValidatorContext, string> action, string message, params Func<T, object>[] funcs)
        {
            Func<T, TProperty, object>[] tmp = funcs.Select(func => new Func<T, TProperty, object>((instance, value) => func(instance))).ToArray();
            return rule.OnFailure(action, message, tmp);
        }

        public static IRuleBuilderOptions<T, TProperty> OnFailure<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Action<T, PropertyValidatorContext, string> action, string message, params Func<T, TProperty, object>[] funcs)
        {
            return rule.Configure(cfg =>
            {
                cfg.ReplaceValidator(cfg.CurrentValidator, new OnFailureValidator<T, TProperty>(cfg.CurrentValidator, action, message, funcs));
            });
        }

        static Func<T, object>[] ConvertArrayOfObjectsToArrayOfDelegates<T>(object[] objects)
        {
            if (objects == null || objects.Length == 0)
            {
                return new Func<T, object>[0];
            }
            return objects.Select(obj => new Func<T, object>(x => obj)).ToArray();
        }
    }
}
