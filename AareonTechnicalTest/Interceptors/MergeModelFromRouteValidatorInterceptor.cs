using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AareonTechnicalTest.Interceptors
{
    /// <summary>
    ///     Merges route parameters into a complex request model after normal model binding has happened.
    /// </summary>
    public class MergeModelFromRouteValidatorInterceptor : IValidatorInterceptor
    {
        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            if (actionContext?.RouteData == null)
            {
                return commonContext;
            }

            if (commonContext?.InstanceToValidate == null)
            {
                return commonContext;
            }

            foreach (var routeDataValue in actionContext.RouteData.Values)
            {
                var property = commonContext.InstanceToValidate.GetType().GetProperty(routeDataValue.Key);
                if (property != null)
                {
                    try
                    {
                        var typedValue = property.PropertyType.IsEnum ?
                            Enum.Parse(property.PropertyType, routeDataValue.Value.ToString(), true)
                            : Convert.ChangeType(routeDataValue.Value, property.PropertyType);

                        property.SetValue(commonContext.InstanceToValidate, typedValue);
                    }
                    catch (Exception)
                    {
                        throw new FormatException(
                            $"{property.PropertyType.Name} property {commonContext.InstanceToValidate.GetType().Name}.{property.Name}" +
                            $" could not be set using route value {routeDataValue.Value}");
                    }
                }
            }

            return commonContext;
        }

        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            return result;
        }
    }
}