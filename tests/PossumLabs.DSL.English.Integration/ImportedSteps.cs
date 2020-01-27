using BoDi;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.English.Integration
{
    public class FrameworkInitializationSteps : PossumLabs.DSL.English.FrameworkInitializationSteps
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class AlertSteps : PossumLabs.DSL.English.AlertSteps
    {
        public AlertSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class DriverSteps : PossumLabs.DSL.English.DriverSteps
    {
        public DriverSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class ErrorSteps : PossumLabs.DSL.English.ErrorSteps
    {
        public ErrorSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class LogSteps : PossumLabs.DSL.English.LogSteps
    {
        public LogSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class ResolvedStringSteps : PossumLabs.DSL.English.ResolvedStringSteps
    {
        public ResolvedStringSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class TableSteps : PossumLabs.DSL.English.TableSteps
    {
        public TableSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class ValidationSteps : PossumLabs.DSL.English.ValidationSteps
    {
        public ValidationSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }
    public class WebValidationSteps : PossumLabs.DSL.English.WebValidationSteps
    {
        public WebValidationSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }
}
