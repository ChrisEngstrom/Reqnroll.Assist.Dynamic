﻿using System;
using System.Collections.Generic;

namespace Reqnroll.Assist;

public class DynamicInstanceComparisonException : Exception
{
    public IList<string> Differences { get; private set; }

    public DynamicInstanceComparisonException(IList<string> diffs)
        : base("There were some differences between the table and the instance")
    {
        Differences = diffs;
    }
}