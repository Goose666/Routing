﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Abstractions;

namespace Microsoft.AspNet.Routing.Template
{
    public class TemplateRoute : IRouter
    {
        private readonly IDictionary<string, object> _defaults;
        private readonly IRouter _target;
        private readonly Template _parsedTemplate;
        private readonly string _routeTemplate;
        private readonly TemplateMatcher _matcher;
        private readonly TemplateBinder _binder;

        public TemplateRoute(IRouter target, string routeTemplate)
            : this(target, routeTemplate, null)
        {
        }

        public TemplateRoute(IRouter target, string routeTemplate, IDictionary<string, object> defaults)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            _target = target;
            _routeTemplate = routeTemplate ?? string.Empty;
            _defaults = defaults ?? new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            // The parser will throw for invalid routes.
            _parsedTemplate = TemplateParser.Parse(RouteTemplate);

            _matcher = new TemplateMatcher(_parsedTemplate);
            _binder = new TemplateBinder(_parsedTemplate);
        }

        public IDictionary<string, object> Defaults
        {
            get { return _defaults; }
        }

        public string RouteTemplate
        {
            get { return _routeTemplate; }
        }

        public async virtual Task RouteAsync(RouteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var requestPath = context.RequestPath;
            if (!string.IsNullOrEmpty(requestPath) && requestPath[0] == '/')
            {
                requestPath = requestPath.Substring(1);
            }

            var values = _matcher.Match(requestPath, _defaults);
            if (values == null)
            {
                // If we got back a null value set, that means the URI did not match
                return;
            }
            else
            {
                await _target.RouteAsync(new RouteContext(context.HttpContext){ Values = values });
            }
        }

        public void BindPath(BindPathContext context)
        {
            // This could be optimized more heavily - right now we try to do the full url
            // generation before validating, but we could do it in two phases.
            var path = _binder.Bind(_defaults, context.AmbientValues, context.Values);
            if (path == null)
            {
                return;
            }

            context.BoundPath = path;
            _target.BindPath(context);

            if (!context.IsBound)
            {
                context.BoundPath = null;
            }
        }
    }
}