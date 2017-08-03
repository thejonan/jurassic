// Copyright (c) COZYROC, LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace Jurassic.Importer
{
    public class ImportedModule
    {
        private Dictionary<EntityHandle, ImportedEntity> _resolvedEntities = new Dictionary<EntityHandle, ImportedEntity>();

        private MetadataReader _reader;
        private string _assemblyName;

        public static ImportedModule Create(IntPtr metadataBlock, uint blockSize)
        {
            return new ImportedModule(metadataBlock, blockSize);
        }

        internal unsafe ImportedModule(IntPtr metadataBlock, uint blockSize)
        {
            _reader = new MetadataReader((byte*)metadataBlock, (int)blockSize);
        }

        public string AssemblyName
        {
            get
            {
                if (_assemblyName == null)
                {
                    AssemblyDefinition assemblyDef = _reader.GetAssemblyDefinition();
                    _assemblyName = _reader.GetString(assemblyDef.Name);
                }

                return _assemblyName;
            }
        }

        public MetadataReader MetaReader { get { return _reader;  } }

        public ImportedEntity GetFunction (int tokenId)
        {
            MethodDefinitionHandle handle = (MethodDefinitionHandle)MetadataTokens.EntityHandle(tokenId);
            ImportedEntity method;
            if (!_resolvedEntities.TryGetValue(handle, out method))
            {
                MethodDefinition methodDef = _reader.GetMethodDefinition(handle);
                method = new ImportedEntity(this, methodDef);
                _resolvedEntities.Add(handle, method);
            }

            return method;
        }

        public ImportedEntity GetObject()
        {
            // TODO: Check what parameters need to be sent here.
            return null;
        }
    }
}
