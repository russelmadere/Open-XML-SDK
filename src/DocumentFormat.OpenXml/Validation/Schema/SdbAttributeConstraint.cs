﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

/**********************************************************
 * Define data struct for schema constraint binary database
 **********************************************************/

using System;

using SdbIndex = System.UInt16;

namespace DocumentFormat.OpenXml.Validation.Schema
{
    /// <summary>
    /// Attribute constraint data.
    /// </summary>
    internal class SdbAttributeConstraint : SdbData
    {
        /// <summary>
        /// Gets or sets the xsd:use value.
        /// </summary>
        public XsdAttributeUse AttributeUse { get; set; }

        /// <summary>
        /// Gets or sets the index of the simple data in the SdbSimpleTypeRestriction data array.
        /// </summary>
        public ushort SimpleTypeIndex { get; set; }

        /// <summary>
        /// Gets or sets in which file format version this attribute is allowed.
        /// </summary>
        public byte FileFormatVersion { get; set; }

        public SdbAttributeConstraint()
        {
        }

        public SdbAttributeConstraint(XsdAttributeUse xsdAttributeUse, ushort simpleTypeIndex, byte fileFormatVersion)
        {
            AttributeUse = xsdAttributeUse;
            SimpleTypeIndex = simpleTypeIndex;
            FileFormatVersion = fileFormatVersion;
        }

        /// <summary>
        /// Gets the size in bytes of this data structure.
        /// </summary>
        public static int TypeSize
        {
            get
            {
                // We save the enum data in byte
                return sizeof(XsdAttributeUse) + sizeof(ushort) + sizeof(byte);
            }
        }

        #region Override SdbData Members

        /// <summary>
        /// Gets the size in bytes of this data structure.
        /// </summary>
        public override int DataSize => TypeSize;

        /// <summary>
        /// Serialize the data into byte data.
        /// </summary>
        /// <returns>Byte data.</returns>
        public override byte[] GetBytes()
        {
            return GetBytes(AttributeUse.Bytes(),
                                SimpleTypeIndex.Bytes(),
                                FileFormatVersion.Bytes());
        }

        /// <summary>
        /// Deserialize the data from byte data.
        /// </summary>
        /// <param name="value">The byte data.</param>
        /// <param name="startIndex">The offset the data begins at.</param>
        public override void LoadFromBytes(byte[] value, int startIndex)
        {
            AttributeUse = (XsdAttributeUse)LoadByte(value, ref startIndex);
            SimpleTypeIndex = LoadSdbIndex(value, ref startIndex);
            FileFormatVersion = LoadByte(value, ref startIndex);
        }

        #endregion
    }
}
