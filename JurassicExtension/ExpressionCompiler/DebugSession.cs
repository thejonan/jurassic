// Copyright (c) COZYROC LLc. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.VisualStudio.Debugger.Evaluation.ClrCompilation;
using Microsoft.VisualStudio.Debugger.Evaluation;
using Microsoft.VisualStudio.Debugger;
using Microsoft.VisualStudio.Debugger.Clr;
using Microsoft.VisualStudio.Debugger.Symbols;
using Jurassic.Compiler;
using Jurassic;
using Jurassic.Importer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JurassicExtension.ExpressionCompiler
{
    /// <summary>
    /// An exapsulation of
    /// </summary>
    internal class DebugSession : DkmDataItem, IDisposable
    {
        public readonly List<string> FormatSpecifiers = new List<string>();

        /// <summary>
        /// The inspection-related stuff to store.
        /// </summary>
        public readonly DkmClrInstructionAddress InstructionAddress;
        public readonly DkmInspectionContext InspectionContext;

        private Dictionary<DkmClrInstructionAddress, DebugSession> _sessions;

        private ImportedModule  _module;
        private ImportedScope   _inspectionScope;
        private ImportedEntity  _currentThis;
        private ImportedEntity  _currentFunction;
        private ScriptEngine    _engine;

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

        protected DebugSession(
            DkmInspectionContext inspectionContext,
            DkmClrInstructionAddress instructionAddress
            )
        {
            InspectionContext = inspectionContext;
            InstructionAddress = instructionAddress;
            BuildContext();
        }

        public Scope Scope { get { return _inspectionScope; } }
        public ImportedEntity Function { get { return _currentFunction; } }
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

        public ImportedEntity[] FunctionArguments
        {
            get
            {
                // TODO: Use _currentFunction
                return null;
            }
        }

        public ImportedEntity[] LocalVariables
        {
            get
            {
                //                return Scope.GetLocals().Select(v => v.Variable).ToArray();
                return null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _engine = null;
                _currentThis = null;
                _currentFunction = null;
                _inspectionScope = null;
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

        /// <summary>
        /// Initialize the full context, including the scope, this variable, etc.
        /// </summary>
        private void BuildContext()
        {
            IntPtr metadataBlock;
            uint blockSize;
            try
            {
                metadataBlock = InstructionAddress.ModuleInstance.GetMetaDataBytesPtr(out blockSize);
            }
            catch (DkmException)
            {
                // This can fail when dump debugging if the full heap is not available
                return;
            }

            _module = ImportedModule.Create(metadataBlock, blockSize);
            _currentFunction = _module.GetFunction(InstructionAddress.MethodId.Token);
            _currentThis = _module.GetObject();
        }
    }
}
