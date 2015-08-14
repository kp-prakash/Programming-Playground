using System;

namespace AOP.WcfAspects.Aspects
{
    /// <summary>
    /// Use this attribute to mark the properties of a DataContract
    /// which should not be decorated with a DataMemberAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NotADataMemberAttribute : Attribute
    {
    }
}