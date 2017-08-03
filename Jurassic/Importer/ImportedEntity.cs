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
    public class ImportedEntity
    {
        public readonly ImportedModule Module;
        public readonly ImportedScope Scope;

        public ImportedEntity(ImportedModule module, MethodDefinition method)
        {
            Module = module;
            Scope = null;
        }

        public string Name
        {
            get;
        }

        public ImportedEntity Prototype
        {
            get;
        }

        public object Value
        {
            get;
        }
    }
}
