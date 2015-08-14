using System;
using System.Collections.Generic;
using System.Linq;
using PostSharp;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace AOP.Validation.And.CodeGeneration.Aspects
{
    /// <summary>
    /// There are three important rules to be validated as part of NHibernate usage.
    /// NHibernate requires the methods and properties to be declared as virtual and also needs an
    /// no argument constructor. When any of these are missing NHibernate throws runtime exception.
    /// The intent of this PostSharp TypeLevelAspect is to ensure that these are captured during compile time.
    /// IValidableAnnotation is part of TypeLevelAspect, just included here for clarity. IValidableAnnotation
    /// exposes the method CompileTimeValidate(Type type)
    /// </summary>
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Class)]
    public sealed class NHibernateEntityValidatingAspect : TypeLevelAspect, IValidableAnnotation
    {
        /// <summary>
        /// Compile time validation.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns 'false' if validation fails.</returns>
        public override bool CompileTimeValidate(Type type)
        {
            var error = true;
            var processedNames = new List<string>();
            var methods = type.GetMethods();
            var selectedMethods = methods.Where(
                x => !x.IsVirtual
                && string.Compare(x.Name, "GetType", true) != 0);

            //virtual flag cannot be directly checked on properties hence we use methods to obtain the same.
            //This code below takes care of both properties and methods.
            foreach (var method in selectedMethods)
            {
                var processedMethodName = method.Name.Replace("get_", string.Empty).Replace("set_", string.Empty);
                if (!processedNames.Contains(processedMethodName))
                {
                    processedNames.Add(processedMethodName);
                    Message.Write(MessageLocation.Of(method), SeverityType.Error, "E001",
                                  string.Format("{0}.{1} is not virtual", type.Name, processedMethodName));
                    error = false;
                }
            }

            //Checks if there is a constructor with zero parameter.
            if (type.GetConstructors().Where(x => x.GetParameters().Count() == 0).Count() != 1)
            {
                Message.Write(MessageLocation.Of(type), SeverityType.Error, "E002",
                              string.Format("{0} needs a zero parameter constructor", type.Name));
                error = false;
            }

            return error;
        }
    }
}