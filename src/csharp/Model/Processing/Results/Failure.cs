﻿// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace DewIt.Model.Processing.Results;

public class Failure : IResult
{
    public string Objective { get; }
    public ResultCode Code => ResultCode.FAILURE;
    public string Reason { get; }
    public List<Exception> Exceptions { get; }
    
    public Failure(string objective, Exception ex) 
        : this(objective, null, ex.ToList())
    {
    }

    public Failure(string objective, string reason, Exception ex) 
        : this(objective, reason, ex.ToList())
    {
    }
    
    public Failure(string objective, string? reason, IEnumerable<Exception> exceptions)
    {
        var exceptionsList = exceptions.ToList();
        if (reason == null && !exceptionsList.Any())
        {
            throw new ArgumentException(ProcessingStrings.ERROR_CannotCreateFailureWithoutReasonOrException);
        }
        
        Objective = objective;
        Exceptions = exceptionsList;
        Reason = reason ?? exceptionsList.First().Message;
    }

    public static implicit operator bool(Failure result)
    {
        if (result == null) throw new ArgumentNullException(nameof(result));
        return false;
    }
}

public class Failure<TOut> : Failure, IResult<TOut>
{
    public TOut? Output { get; }

    public Failure(string objective, string reason, Exception ex) 
        : this(objective, default, reason, ex.ToList())
    {
    }

    public Failure(string objective, string reason, IEnumerable<Exception> exceptions) 
        : this(objective, default, reason, exceptions)
    {
    }

    public Failure(string objective, TOut? output, string reason, Exception ex) 
        : this(objective, output, reason, ex.ToList())
    {
    }


    public Failure(string objective, TOut? output, string? reason, IEnumerable<Exception> exceptions) 
        : base(objective, reason, exceptions)
    {
        Output = output ?? default;
    }

    public static implicit operator bool(Failure<TOut> d) => d.Code != ResultCode.FAILURE;
}
