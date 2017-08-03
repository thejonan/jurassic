// Copyright (c) COZYROC, LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic.Compiler;

namespace Jurassic.Importer
{
    public class ImportedScope
    {
        public readonly ImportedModule Module;
        public readonly Scope Scope;
        public List<ImportedEntity> _variables;

        public ImportedScope(ImportedModule module)
        {
            Module = module;
            Scope = null;
            _variables = null;
        }

        public List<ImportedEntity> Variables
        {
            get
            {
                if (_variables == null)
                {
                    // TODO: Build the list of variables in this scope.
                }
                return _variables;
            }

        }
    }
}
