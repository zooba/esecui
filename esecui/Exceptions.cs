﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace esecui
{
    class PythonCompilationException : Exception
    {
        public PythonCompilationException(Exception innerException)
            : base("Error compiling Python code", innerException)
        { }

    }

    class EvaluatorCompilationException : Exception
    {
        public EvaluatorCompilationException(Exception innerException)
            : base("Error compiling evalutor code", innerException)
        { }

    }

    class ESDLCompilationException : Exception
    {
        public ESDLCompilationException(Exception innerException)
            : base("Error compiling ESDL code", innerException)
        { }
    }

}
