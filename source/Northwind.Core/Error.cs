namespace Northwind.Core;

public class Error : IEquatable<Error>
{
  public static readonly Error None = new(string.Empty, string.Empty);
  public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

  public Error(string codeParam, string messageParam)
  {
    Code = codeParam;
    Message = messageParam;
  }

  public string Code { get; }

  public string Message { get; }

  public bool Equals(Error? otherParam)
  {
    if (ReferenceEquals(null, otherParam))
    {
      return false;
    }

    if (ReferenceEquals(this, otherParam))
    {
      return true;
    }

    return Code == otherParam.Code && Message == otherParam.Message;
  }

  public override bool Equals(object? objParam)
  {
    if (ReferenceEquals(null, objParam))
    {
      return false;
    }

    if (ReferenceEquals(this, objParam))
    {
      return true;
    }

    if (objParam.GetType() != GetType())
    {
      return false;
    }

    return Equals((Error)objParam);
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Code, Message);
  }

  public static bool operator ==(Error? leftParam, Error? rightParam)
  {
    return Equals(leftParam, rightParam);
  }

  public static implicit operator string(Error errorParam)
  {
    return errorParam.Code;
  }

  public static bool operator !=(Error? leftParam, Error? rightParam)
  {
    return !Equals(leftParam, rightParam);
  }
}