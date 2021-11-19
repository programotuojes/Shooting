namespace API.Authorization {
  using System;

  [AttributeUsage(AttributeTargets.Method)]
  public class AllowAnonymousAttribute : Attribute { }
}
