// Copyright (c) Microsoft Corporation.  All rights reserved.

//Copyright (c) Microsoft Corporation.  All rights reserved.
using System;

namespace Microsoft.SDK.Samples.VistaBridge.Library.PowerManagement
{
    /// <summary>
    /// Specifies the supported power personalities.  
    /// </summary>
    public enum PowerPersonality
    {
        /// <summary>
        /// Power settings designed to deliver maximum performance
        /// at the expense of power consumption savings.
        /// </summary>
        HighPerformance,

        /// <summary>
        /// Power settings designed consume minimum power
        /// at the expense of system performance and responsiveness.
        /// </summary>
        PowerSaver,

        /// <summary>
        /// Power settings designed to balance performance 
        /// and power consumption.
        /// </summary>
        Automatic
    }

}