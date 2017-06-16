// Copyright (c) COZYROC, LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jurassic.Importer
{
    public class ImportedModule
    {
        public static ImportedModule Create(IntPtr metadataBlock, uint blockSize)
        {
            return null;
        }

        public ImportedEntity GetFunction (int tokenId)
        {
            return null;
        }

        public ImportedEntity GetObject()
        {
            // TODO: Check what parameters need to be sent here.
            return null;
        }
    }
}
