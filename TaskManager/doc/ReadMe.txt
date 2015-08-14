A List of User Operations
/api/users GET Gets the full list of all users; optionally specifies a filter
/api/users/123 GET Gets the details for a single user

A List of Task Operations
/api/tasks GET Gets the full list of all tasks; optionally specify a filter
/api/tasks/123 GET Gets the details for a single task
/api/tasks/123/users GET Gets the users assigned to the specified task
/api/tasks/123/users PUT Replaces all users on the specified task; returns the updated task in the response
/api/tasks/123/users DELETE Deletes all users from the specified task; returns the updated task in the response
/api/tasks/123/users/456 PUT Adds the specified user (e.g., 456) as an assignee on the task; returns the updated task in the response
/api/tasks/123/users/456 DELETE Deletes the specified user from the assignee list; returns the updated task in the response
/api/tasks POST Creates a new task; returns the new task in the response
/api/tasks/123 PUT Updates the specified task; returns the updated task in the response

A List of Task Status Operations
/api/tasks/123/activations POST Starts, or “activates,” a task; returns the updated task in the response
/api/tasks/123/completions POST Completes a task; returns the updated task in the response
/api/tasks/123/reactivations POST Reopens, or “re-activates,” a task; returns the updated task in the response

Nuget Packages

update-package Microsoft.AspNet.WebApi TaskManager.Web.Api
install-package automapper TaskManager.Common
install-package log4net TaskManager.Common
install-package nhibernate TaskManager.Data.SqlServer
install-package fluentnhibernate TaskManager.Data.SqlServer
install-package automapper TaskManager.Web.Api
install-package log4net TaskManager.Web.Api
install-package nhibernate TaskManager.Web.Api
install-package fluentnhibernate TaskManager.Web.Api
install-package Ninject.Web.Common.WebHost TaskManager.Web.Api
install-package log4net TaskManager.Web.Common
install-package nhibernate TaskManager.Web.Common
install-package ninject TaskManager.Web.Common
install-package ninject.web.common TaskManager.Web.Common

Log4Net Logging
There are about 101 ways to configure logging with log4net. If you want to log a target other than a rolling
log file, or if you are interested in modifying the behavior just covered, you should read the log4net configuration
documentation to learn more. Here are a couple of useful links:
• http://logging.apache.org/log4net/release/manual/configuration.html
• http://logging.apache.org/log4net/release/sdk/log4net.Layout.PatternLayout.html


API Versioning using Namespaces!
Read this article: http://blogs.msdn.com/b/webdev/archive/2013/03/08/using-namespaces-to-version-web-apis.aspx
And look into this code: http://aspnet.codeplex.com/SourceControl/changeset/view/dd207952fa86#Samples/WebApi/NamespaceControllerSelector/NamespaceHttpControllerSelector.cs
For Web API Samples, see: http://www.asp.net/aspnet/samples/aspnet-web-api and http://aspnet.codeplex.com/SourceControl/latest
----------------------------------------------------------------------------------------------------------------------------
﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace NamespaceControllerSelectorSample
{
    public class NamespaceHttpControllerSelector : IHttpControllerSelector
    {
        private const string NamespaceKey = "namespace";
        private const string ControllerKey = "controller";

        private readonly HttpConfiguration _configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;
        private readonly HashSet<string> _duplicates;

        public NamespaceHttpControllerSelector(HttpConfiguration config)
        {
            _configuration = config;
            _duplicates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

            // Create a lookup table where key is "namespace.controller". The value of "namespace" is the last
            // segment of the full namespace. For example:
            // MyApplication.Controllers.V1.ProductsController => "V1.Products"
            IAssembliesResolver assembliesResolver = _configuration.Services.GetAssembliesResolver();
            IHttpControllerTypeResolver controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();

            ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            foreach (Type t in controllerTypes)
            {
                var segments = t.Namespace.Split(Type.Delimiter);

                // For the dictionary key, strip "Controller" from the end of the type name.
                // This matches the behavior of DefaultHttpControllerSelector.
                var controllerName = t.Name.Remove(t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);

                var key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", segments[segments.Length - 1], controllerName);

                // Check for duplicate keys.
                if (dictionary.Keys.Contains(key))
                {
                    _duplicates.Add(key);
                }
                else
                {
                    dictionary[key] = new HttpControllerDescriptor(_configuration, t.Name, t);  
                }
            }

            // Remove any duplicates from the dictionary, because these create ambiguous matches. 
            // For example, "Foo.V1.ProductsController" and "Bar.V1.ProductsController" both map to "v1.products".
            foreach (string s in _duplicates)
            {
                dictionary.Remove(s);
            }
            return dictionary;
        }

        // Get a value from the route data, if present.
        private static T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result = null;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T)result;
            }
            return default(T);
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            IHttpRouteData routeData = request.GetRouteData();
            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Get the namespace and controller variables from the route data.
            string namespaceName = GetRouteVariable<string>(routeData, NamespaceKey);
            if (namespaceName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string controllerName = GetRouteVariable<string>(routeData, ControllerKey);
            if (controllerName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Find a matching controller.
            string key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, controllerName);

            HttpControllerDescriptor controllerDescriptor;
            if (_controllers.Value.TryGetValue(key, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            else if (_duplicates.Contains(key))
            {
                throw new HttpResponseException(
                    request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "Multiple controllers were found that match this request."));
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers.Value;
        }
    }
}

----------------------------------------------------------------------------------------------------------------------------
SOLID Principles
S - http://www.objectmentor.com/resources/articles/srp.pdf
O - http://www.objectmentor.com/resources/articles/ocp.pdf
L - http://www.objectmentor.com/resources/articles/lsp.pdf
I - http://www.objectmentor.com/resources/articles/isp.pdf
D - http://www.objectmentor.com/resources/articles/dip.pdf

The easiest way to approach this is summarized in these two points:
• Push dependencies up to the constructor.
• Configure the application to use dependency injection.

Ninject-Related Activities
--------------------------

Container configuration 
NinjectWebCommon
Make sure a DI container is created during application start-up and remains in memory until the application shuts down. (You can think of the container as the object that contains the dependencies.)

Container bindings
NinjectConfigurator
This is where we bind or relate interfaces to concrete implementations so that the dependencies can be resolved at run time. For example, if a class requires an IDateTime object, the bindings tell the container to provide a DateTimeAdapter object.

Dependency resolver for Ninject
NinjectDependencyResolver
This tells ASP.NET Web API to ask Ninject for all dependencies required at run time by the dependent objects. This is the key that allows you to push dependencies up to the constructor on he controllers. Without this resolver, ASP.NET won’t use your configured Ninject container for dependencies.

Scopes
------
InSingletonScope - Creates and maintains a singleton.
InRequestScope - Create a new object for every request.
ToConstant - Always returns the bound instance. We create the object and map bind it!
Call to XmlConfigurator.Configure is required to configure log4net

Database Configuration Overview
-------------------------------
As with any approach to data access, at some point the underlying framework must be told how to connect to the database. And because NHibernate is database-provider agnostic, we must also tell it which provider we’re using, and even which version of which provider.

TaskManager.Data - includes entire domain model and this is not dependent on any database provider.
TaskManager.Data.SqlServer project, which contains the NHibernate mapping definitions. These could possibly change when swapping out database providers.

Actually wiring-in the database configuration with the ASP.NET Web API framework requires a small bit of code located in the application’s start-up logic, along with some supporting classes (that are easy to isolate) and some config file-based configuration. That means deciding to switch from SQL Server to Oracle, for example, can be accomplished relatively noninvasively.

The first thing you might notice is that all of the mapping code is contained within each class’s constructor.
Second, notice the use of the VersionedClassMap<T> base class for each of the map classes.
This custom class leverages NHibernate’s ability to check for dirty records in the database, based on a Rowversion column on each table.

The crazy-long statement in the VersionedClassMap implementation can be broken down as follows:
• Use the Version property on each entity class as a concurrency (or, version) value.
• The database column supporting versioning is named ts.
• The SQL data type is a Rowversion.
• NHibernate should always let the database generate the value, as opposed to you or NHibernate supplying the value.
• Prior to a database save, the in-memory value of the Version property will be null.

ClassMap<T>, the base class of VersionedClassMap<T>, is defined in the Fluent NHibernate library, and it simply provides a means of configuring entity-to-database mapping through code (as opposed to using XML files). This mapping code is placed in each mapping class’s constructor.

Id method can be called only once, and it’s used to tell NHibernate which property on the entity class is used as the object identifier.
The Map is used to configure individual properties on the entities. By default, NHibernate will assume the mapped column name is the same as the given property name. If it’s not, an overload can be used to specify the column name. Additionally, because this is a fluent-style interface, we can chain other property and column specifics together.

