namespace API.Authorization {
  using System;

  public class AppException : Exception {

    public AppException(string message) : base(message) { }
  }
}
