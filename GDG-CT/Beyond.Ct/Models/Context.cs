// ***********************************************************************
// Author           : Anugoon Leelaphattarakij
// ***********************************************************************
// <copyright file="Tag.cs" company="CT">
//     Copyright (c) Anugoon Leelaphattarakij. All rights reserved.
// </copyright>
// ***********************************************************************

using Beyond.Data;

namespace Beyond.Ct.Models
{
    /// <summary>
    /// Data model represents a Tag.
    /// </summary>
    public class Tag : Entity
    {
        #region entity properties

        /// <summary>
        /// Gets or sets the name of this <see cref="Tag"/>.
        /// </summary>
        /// <value>The name of the <see cref="Tag"/>.</value>
        public string Name { get; set; }

        #endregion

        #region related entities

        #endregion

        #region constructor and disposable handler

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        public Tag()
        {
        }

        #endregion constructor and disposable handler
    }
}
