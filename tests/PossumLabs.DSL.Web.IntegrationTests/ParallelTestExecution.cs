using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: Parallelize(Workers = 8, Scope = ExecutionScope.ClassLevel)]

namespace PossumLabs.DSL.Web.Integration
{
    class ParallelTestExecution
    {
    }
}
