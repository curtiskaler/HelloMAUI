﻿using DewIt.Model.DataTypes;

namespace DewIt.Model.CommandLine.Model
{
    public class ExitCode : Enumeration<ExitCode>
    {
        protected ExitCode(int ordinal, string name) : base(ordinal, name)
        {
        }

        public static readonly ExitCode Success = new ExitCode(0, "SUCCESS");
    }
}