using System;
using AOP.Validation.And.CodeGeneration.DomainModel;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Extensibility;

namespace AOP.Validation.And.CodeGeneration.Aspects
{
    /// <summary>
    /// Custom interface.
    /// </summary>
    public interface INHibernateEquality
    {
        bool Equals(object item);

        int GetHashCode();
    }

    /// <summary>
    /// This aspect helps us override Equals() and GetHashCode()
    /// methods for all the classes. This assumes that the equality
    /// is based on the Id property. Typically this works / helps in equating
    /// two instances of NHibernate entities.
    /// </summary>
    [Serializable]
    [IntroduceInterface(typeof(INHibernateEquality), OverrideAction = InterfaceOverrideAction.Ignore)]
    [MulticastAttributeUsage(MulticastTargets.Class)]
    public class NHibernateEqualityAspect : InstanceLevelAspect, INHibernateEquality
    {
        //Get the value from the Id property and pass it into this aspect.
        [ImportMember("Id", IsRequired = true,
            Order = ImportMemberOrder.BeforeIntroductions)]

        //Take the Id and expose it is a Property<Guid>
        public Property<Guid> Id;

        private Type _baseType;

        public override void CompileTimeInitialize(Type type, AspectInfo aspectInfo)
        {
            _baseType = type;
        }

        //Introduce a member.
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [IntroduceMember(IsVirtual = true,
            OverrideAction = MemberOverrideAction.OverrideOrFail)]
        public override bool Equals(object obj)
        {
            if (obj.GetType() != _baseType) return false;
            var incomingObj = obj as Entity;
            if (incomingObj == null) return false;
            return Id.Get.Invoke().Equals(incomingObj.Id);
        }

        //Introduce a member.
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        [IntroduceMember(IsVirtual = true,
            OverrideAction = MemberOverrideAction.OverrideOrFail)]
        public override int GetHashCode()
        {
            return Id.Get.Invoke().GetHashCode();
        }
    }
}