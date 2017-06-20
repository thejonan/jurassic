// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Jurassic;
using Microsoft.VisualStudio.Debugger;
using Microsoft.VisualStudio.Debugger.CallStack;
using Microsoft.VisualStudio.Debugger.ComponentInterfaces;
using Microsoft.VisualStudio.Debugger.Evaluation;
using System;
using System.Text;

namespace JurassicExtension.FrameDecoder
{
    /// <summary>
    /// This class is the entry point into the Frame Decoder.  The frame decoder is used to provide
    /// the text shown in the Call Stack window or other places in the debugger UI where stack
    /// frames are used.  See the method comments below for more details about each method.
    /// </summary>
    public sealed class JurassicFrameDecoder : IDkmLanguageFrameDecoder
    {
        /// <summary>
        /// This method is called by the debug engine to get the text representation of a stack
        /// frame.
        /// </summary>
        /// <param name="inspectionContext">Context of the evaluation.  This contains options/flags
        /// to be used during compilation. It also contains the InspectionSession.  The inspection
        /// session is the object that provides lifetime management for our objects.  When the user
        /// steps or continues the process, the debug engine will dispose of the inspection session</param>
        /// <param name="workList">The current work list.  This is used to batch asynchronous
        /// work items.  If any asynchronous calls are needed later, this is the work list to pass
        /// to the asynchronous call.  It's not needed in our case.</param>
        /// <param name="frame">The frame to get the text representation for</param>
        /// <param name="argumentFlags">Option flags to change the way we format frames</param>
        /// <param name="completionRoutine">Completion routine to call when work is completed</param>
        void IDkmLanguageFrameDecoder.GetFrameName(
            DkmInspectionContext inspectionContext,
            DkmWorkList workList,
            DkmStackWalkFrame frame,
            DkmVariableInfoFlags argumentFlags,
            DkmCompletionRoutine<DkmGetFrameNameAsyncResult> completionRoutine)
        {
            string name = TryGetFrameNameHelper(inspectionContext, frame, argumentFlags) ?? "<Unknown Method>";
            completionRoutine(new DkmGetFrameNameAsyncResult(name));
        }

        /// <summary>
        /// This method is called by the debug engine to get the text representation of the return
        /// value of a stack frame.
        /// </summary>
        /// <param name="inspectionContext">Context of the evaluation.  This contains options/flags
        /// to be used during compilation. It also contains the InspectionSession.  The inspection
        /// session is the object that provides lifetime management for our objects.  When the user
        /// steps or continues the process, the debug engine will dispose of the inspection session</param>
        /// <param name="workList">The current work list.  This is used to batch asynchronous
        /// work items.  If any asynchronous calls are needed later, this is the work list to pass
        /// to the asynchronous call.  It's not needed in our case.</param>
        /// <param name="frame">The frame to get the text representation of the return value for</param>
        /// <param name="completionRoutine">Completion routine to call when work is completed</param>
        void IDkmLanguageFrameDecoder.GetFrameReturnType(
            DkmInspectionContext inspectionContext,
            DkmWorkList workList,
            DkmStackWalkFrame frame,
            DkmCompletionRoutine<DkmGetFrameReturnTypeAsyncResult> completionRoutine)
        {
            completionRoutine(new DkmGetFrameReturnTypeAsyncResult(TryGetFrameReturnTypeHelper(inspectionContext, frame)));
        }

        private static string TryGetFrameReturnTypeHelper(DkmInspectionContext inspectionContext, DkmStackWalkFrame frame)
        {
            return "object";
        }

        private static string TryGetFrameNameHelper(DkmInspectionContext inspectionContext, DkmStackWalkFrame frame, DkmVariableInfoFlags argumentFlags)
        {
            using (DebugSession session = DebugSession.GetInstance(inspectionContext, frame))
            {
                return "Jurassic ()";
            }

        }
    }
}
