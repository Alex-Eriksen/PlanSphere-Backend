using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class HandlerTypeAttribute(HandlerType value) : Attribute
{
    public HandlerType Value { get; } = value;
}