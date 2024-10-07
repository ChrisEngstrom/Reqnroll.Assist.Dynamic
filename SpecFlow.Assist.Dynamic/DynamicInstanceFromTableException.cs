using System;

namespace Reqnroll.Assist;

public class DynamicInstanceFromTableException : Exception
{
    public DynamicInstanceFromTableException(string message) : base(message) { }
}