using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Reflection;

namespace AOP.WcfAspects.Aspects
{
    /// <summary>
    /// Multicast Attibute and derives from a TypeLevelAspect.
    /// It also implements the IAspectProvider interface and ProvideAspects method in it.
    /// </summary>
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Class)]
    public sealed class DataContractAspect : TypeLevelAspect, IAspectProvider
    {
        /// <summary>
        /// Provides new aspects.
        /// </summary>
        /// <param name="targetElement">Code element (<see cref="T:System.Reflection.Assembly" />, <see cref="T:System.Type" />,
        /// <see cref="T:System.Reflection.FieldInfo" />, <see cref="T:System.Reflection.MethodBase" />, <see cref="T:System.Reflection.PropertyInfo" />, <see cref="T:System.Reflection.EventInfo" />,
        /// <see cref="T:System.Reflection.ParameterInfo" />, or <see cref="T:PostSharp.Reflection.LocationInfo" />) to which the current aspect has been applied.</param>
        /// <returns>
        /// A set of aspect instances.
        /// </returns>
        public IEnumerable<AspectInstance> ProvideAspects(object targetElement)
        {
            var targetType = (Type)targetElement;

            //This is to introduce a [DataContract] attribute
            var introduceDataContractAspect =
                new CustomAttributeIntroductionAspect(
                    new ObjectConstruction(
                        typeof(DataContractAttribute)
                        .GetConstructor(Type.EmptyTypes)));

            //This is to introduce a [DataMember] attribute
            var introduceDataMemberAspect =
                new CustomAttributeIntroductionAspect(
                    new ObjectConstruction(
                        typeof(DataMemberAttribute)
                        .GetConstructor(Type.EmptyTypes)));

            //Return an AspectInstance by applying the [DataContract] to the type.
            yield return new AspectInstance(targetType, introduceDataContractAspect);

            var properties = targetType.GetProperties(BindingFlags.Public
                                                        | BindingFlags.DeclaredOnly
                                                        | BindingFlags.Instance);

            foreach (var property in properties)
            {
                //CanWrite set to true and does not have a NotADataMemberAttribute defined on it.
                if (property.CanWrite
                    && !property.IsDefined(typeof(NotADataMemberAttribute), false))
                {
                    //Return an AspectInstance by applying the [DataMember] to the type's property.
                    yield return new AspectInstance(property, introduceDataMemberAspect);
                }
            }
        }
    }
}