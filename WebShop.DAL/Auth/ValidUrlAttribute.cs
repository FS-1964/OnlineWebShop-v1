﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using IModelValidator = Microsoft.AspNetCore.Mvc.ModelBinding.Validation.IModelValidator;

namespace WebShop.DAL.Auth
{
    public class ValidUrlAttribute : Attribute, IModelValidator
    {
        public string ErrorMessage { get; set; }

        IEnumerable<ModelValidationResult> IModelValidator.Validate(ModelValidationContext context)
        {
            var url = context.Model as string;
            if (url != null && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return Enumerable.Empty<ModelValidationResult>();

            return new List<ModelValidationResult> { new ModelValidationResult("", ErrorMessage) };
        }
    }
}
