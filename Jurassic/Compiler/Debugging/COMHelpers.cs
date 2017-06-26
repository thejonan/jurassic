using System;

namespace Jurassic.Compiler
{
    /// <summary>
    /// Used to ease interop with the COM debugging extension.
    /// </summary>
    internal static class COMHelpers
    {
        /// <summary>
        /// Gets the language type GUID for the symbol store.
        /// </summary>
        public static readonly Guid LanguageType =      // JScript
            new Guid("5E02EBCE-95C4-4ABA-A7CD-CD7C3CC7042B");

        /// <summary>
        /// Gets the language vendor GUID for the symbol store.
        /// </summary>
        public static readonly Guid LanguageVendor =
            new Guid("CFA05A92-B7CC-4D3D-92E1-4D18CDACDC8D");
        

        /// <summary>
        /// Gets the document type GUID for the symbol store.
        /// </summary>
        public static readonly Guid DocumentType =
            new Guid("5A869D0B-6611-11D3-BD2A-0000F80849BD");
    }
}
