// Copyright (c) COZYROC LLc. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Jurassic;
using Jurassic.Compiler;
using Jurassic.Importer;
using Microsoft.VisualStudio.Debugger.Evaluation.ClrCompilation;
using Microsoft.VisualStudio.Debugger.Evaluation;
using Microsoft.VisualStudio.Debugger;
using Microsoft.VisualStudio.Debugger.Clr;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Debugger.CallStack;

namespace JurassicExtension
{
    /// <summary>
    /// An exapsulation of
    /// </summary>
    internal class DebugSession : DkmDataItem, IDisposable
    {
        public readonly List<string> FormatSpecifiers = new List<string>();

        // private static Dictionary<DkmInstructionAddress, DebugSession> _sessions;

        /// <summary>
        /// The inspection-related stuff to store.
        /// </summary>
        public readonly DkmInspectionContext InspectionContext;

        private ImportedModule   _module;
        private ImportedScope    _currentScope;
        private ImportedEntity   _currentThis;
        private ImportedFunction _currentFunction;
        private ScriptEngine     _engine;

        /// <summary>
        /// Obtaining a DebugSession instance, either from the stored value in the inspectionContext,
        /// or by creating a new one and storing it into the context.
        /// </summary>
        /// <param name="inspectionContext">The inspection context, as passed from the debugger engine.</param>
        /// <param name="instructionAddress">The breakpoint instruction address to restore the context from.</param>
        /// <returns></returns>
        public static DebugSession GetInstance(
            DkmInspectionContext inspectionContext,
            DkmClrInstructionAddress instructionAddress
            )
        {
            DebugSession session = inspectionContext.InspectionSession.GetDataItem<DebugSession>();
            if (session == null)
            {
                session = new DebugSession(inspectionContext, instructionAddress);
                inspectionContext.InspectionSession.SetDataItem(DkmDataCreationDisposition.CreateNew, session);
            }

            return session;
        }

        public static DebugSession GetInstance(
            DkmInspectionContext inspectionContext,
            DkmStackWalkFrame frame)
        {
            DebugSession session = inspectionContext.InspectionSession.GetDataItem<DebugSession>();
            if (session == null)
            {
                session = new DebugSession(inspectionContext, frame);
                inspectionContext.InspectionSession.SetDataItem(DkmDataCreationDisposition.CreateNew, session);
            }

            return session;

        }

        public static DebugSession GetInstance(DkmInspectionContext inspectionContext)
        {
            return inspectionContext.InspectionSession.GetDataItem<DebugSession>();
        }

        protected DebugSession(
            DkmInspectionContext inspectionContext,
            DkmClrInstructionAddress instructionAddress
            )
        {
            InspectionContext = inspectionContext;
            IntPtr metadataBlock;
            uint blockSize;
            try
            {
                metadataBlock = instructionAddress.ModuleInstance.GetMetaDataBytesPtr(out blockSize);
            }
            catch (DkmException)
            {
                // This can fail when dump debugging if the full heap is not available
                return;
            }

            _module = ImportedModule.Create(metadataBlock, blockSize);
            _currentFunction = _module.GetFunction(instructionAddress.MethodId.Token);
            _currentScope = _currentFunction.Scope;
            _currentThis = _module.GetObject();
        }

        protected DebugSession(
            DkmInspectionContext inspectionContext,
            DkmStackWalkFrame frame
            )
        {
            InspectionContext = inspectionContext;
            // TODO: Build the context from here!
        }

        public ImportedScope Scope { get { return _currentScope; } }
        public ImportedFunction Function { get { return _currentFunction; } }
        public ImportedEntity This { get { return _currentThis; } }

        public ScriptEngine Engine
        {
            get
            {
                if (_engine == null)
                    _engine = new ScriptEngine();
                return _engine;
            }
        }

        public byte[] Emitted
        {
            get
            {
                // TODO: Use the engine's compiler to get these.
                return null;
            }
        }

        public DkmClrCompilationResultFlags ResultFlags
        {
            get;
            set;
        }

        public List<DkmClrLocalVariableInfo> GetArguments()
        {
            return BuildDkmVariableInfo(_currentFunction.Arguments);
        }

        public List<DkmClrLocalVariableInfo> GetVariables()
        {
            return BuildDkmVariableInfo(_currentScope.Variables);
        }

        private List<DkmClrLocalVariableInfo> BuildDkmVariableInfo(List<ImportedEntity> entityList)
        {
            List<DkmClrLocalVariableInfo> infoList = new List<DkmClrLocalVariableInfo>(entityList.Count);
            foreach (var entity in entityList)
            {
                // TODO: Build the DkmVariableInfoList here.
                infoList.Add(null);
            }
            return infoList;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _engine = null;
                _currentThis = null;
                _currentFunction = null;
                _currentScope = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~DebugSession()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }
    }
}
