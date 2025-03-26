using Microsoft.AspNetCore.Components;
using System;

namespace BlazorSample.CustomEvents
{
    public class CustomEventArgs : EventArgs
    {
        public string Detail { get; set; } = string.Empty;
    }
}