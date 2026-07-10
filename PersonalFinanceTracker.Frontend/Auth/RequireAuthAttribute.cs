namespace PersonalFinanceTracker.Frontend.Auth;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class RequireAuthAttribute : Attribute;
