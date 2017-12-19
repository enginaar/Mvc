﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// A <see cref="IModelBinderProvider"/> for types deriving from <see cref="Enum"/>.
    /// </summary>
    public class EnumTypeModelBinderProvider : IModelBinderProvider
    {
        private readonly MvcOptions _options;
        private readonly ILoggerFactory _loggerFactory;

        public EnumTypeModelBinderProvider(MvcOptions options)
            : this(options, NullLoggerFactory.Instance)
        {
        }

        public EnumTypeModelBinderProvider(MvcOptions options, ILoggerFactory loggerFactory)
        {
            _options = options;
            _loggerFactory = loggerFactory;
        }

        /// <inheritdoc />
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.IsEnum)
            {
                return new EnumTypeModelBinder(
                    _options.AllowBindingUndefinedValueToEnumType,
                    context.Metadata.UnderlyingOrModelType,
                    _loggerFactory);
            }

            return null;
        }
    }
}
