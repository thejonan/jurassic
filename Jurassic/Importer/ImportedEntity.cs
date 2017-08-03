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
        public readonly EntityHandle EntityHandle;

        internal ImportedScope _scope;
        internal string _name;

        public ImportedEntity(ImportedModule module, EntityHandle entity)
        {
            Module = module;
            EntityHandle = entity;
            _name = null;
            _scope = null;
        }

        public string Name
        {
            get { return _name;  }
        }

        public ImportedEntity Prototype
        {
            get;
        }

        public object Value
        {
            get;
        }

        virtual public ImportedScope Scope
        {
            get;
        }
    }
}
