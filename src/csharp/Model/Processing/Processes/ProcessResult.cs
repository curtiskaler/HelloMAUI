﻿using DewIt.Model.Processing.Results;

namespace DewIt.Model.Processing.Processes;

public class ProcessResult : IProcessResult
{
    public List<StepAndResult> StepsAndResults { get; protected set; }

    public ProcessResult() : this(new List<StepAndResult>())
    {
    }

    public ProcessResult(IEnumerable<StepAndResult> stepsAndResults)
    {
        StepsAndResults = stepsAndResults.ToList();
    }

    public ResultCode Code
    {
        get
        {
            if (Skipped)
                return ResultCode.SKIP;
            return Failed ? ResultCode.FAILURE : ResultCode.SUCCESS;
        }
    }

    public bool Failed => StepsAndResults.Any(it => it.Failed);
    public bool Skipped => StepsAndResults.All(it => it.Skipped);
    public bool Succeeded => StepsAndResults.All(it => it.Succeeded);
}