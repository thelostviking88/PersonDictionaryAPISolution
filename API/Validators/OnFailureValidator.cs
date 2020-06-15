using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace API.Validators
{
    public class OnFailureValidator<T, TProperty> : NoopPropertyValidator
    {
        private readonly IPropertyValidator _innerValidator;
        private readonly Action<T, PropertyValidatorContext> _onFailureSimple;
        private readonly Action<T, PropertyValidatorContext, string> _onFailureComplex;
        private readonly string _message;
        private readonly Func<T, TProperty, object>[] _funcs;

        public OnFailureValidator(IPropertyValidator innerValidator, Action<T, PropertyValidatorContext, string> onFailure, string message, Func<T, TProperty, object>[] funcs)
        {
            _innerValidator = innerValidator;
            _onFailureComplex = onFailure;
            _message = message;
            _funcs = funcs;
        }

        public OnFailureValidator(IPropertyValidator innerValidator, Action<T, PropertyValidatorContext> onFailure)
        {
            _innerValidator = innerValidator;
            _onFailureSimple = onFailure;
        }

        public override IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            var results = _innerValidator.Validate(context);

            if (results.Any())
            {
                if (string.IsNullOrWhiteSpace(_message))
                {
                    _onFailureSimple((T)context.Instance, context);
                }
                else
                {
                    var messageFormatter = new MessageFormatter();
                    messageFormatter.AppendPropertyName(context.PropertyName);
                    messageFormatter.AppendArgument("PropertyValue", context.PropertyValue);
                    try
                    {
                        messageFormatter.AppendAdditionalArguments(
                            _funcs.Select(func => func((T)context.Instance, (TProperty)context.PropertyValue)).ToArray());
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }

                    var msg = messageFormatter.BuildMessage(_message);
                    _onFailureComplex((T)context.Instance, context, msg);
                }
            }

            return results;
        }
    }
}
