// Copyright (c) COZYROC, LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic.Compiler;
using System.Reflection.Metadata;


namespace Jurassic.Importer
{
    public class ImportedFunction : ImportedEntity
    {
        private readonly MethodDefinition _methodDef;
        private List<ImportedEntity> _arguments;

        public ImportedFunction(ImportedModule module, MethodDefinitionHandle method) : base(module, method)
        {
            _methodDef = Module.MetaReader.GetMethodDefinition((MethodDefinitionHandle)EntityHandle);
            _name = module.MetaReader.GetString(_methodDef.Name);
            _arguments = null;
        }

        public List<ImportedEntity> Arguments
        {
            get
            {
                if (_arguments == null)
                {
                    MetadataReader mdReader = Module.MetaReader;
                    foreach (var parHandle in _methodDef.GetParameters())
                    {
                        Parameter parameter = mdReader.GetParameter(parHandle);
                        // TODO: Build an ImportedEntity and add it to the list.
                    }

                }
                return _arguments;
            }
        }

        override public ImportedScope Scope
        {
            get
            {
                if (_scope == null)
                {
                    _scope = new ImportedScope(Module);
                }
                return _scope;
            }
        }
    }
}
