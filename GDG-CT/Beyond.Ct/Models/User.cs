// ***********************************************************************
// Author           : Anugoon Leelaphattarakij
// ***********************************************************************
// <copyright file="User.cs" company="CT">
//     Copyright (c) Anugoon Leelaphattarakij. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using Beyond.Data;

namespace Beyond.Ct.Models
{
    /// <summary>
    /// Data model represents a User.
    /// </summary>
    public abstract class User : Resource
    {
        #region entity properties

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the encrypted password.
        /// </summary>
        /// <value>The encrypted password.</value>
        [Required]
        public string Password { get; set; }

        #endregion

        #region related entities

        #endregion

        #region constructor and disposable handler

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        #endregion constructor and disposable handler
    }

    /// <summary>
    /// Data model represents a person as a <see cref="User"/>.
    /// </summary>
    public class PersonUser : User
    {
        #region entity properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the of the middle name.
        /// </summary>
        /// <value>The middle name.</value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required]
        public string LastName { get; set; }

        #endregion

        #region related entities

        #endregion

        #region constructor and disposable handler

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonUser"/> class.
        /// </summary>
        public PersonUser()
        {
        }

        #endregion constructor and disposable handler
    }

    /// <summary>
    /// Data model represents a computer system as a <see cref="User"/>.
    /// </summary>
    public class SystemUser : User
    {
        #region entity properties

        #endregion

        #region related entities

        #endregion

        #region constructor and disposable handler

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemUser"/> class.
        /// </summary>
        public SystemUser()
        {
        }

        #endregion constructor and disposable handler
    }
}
