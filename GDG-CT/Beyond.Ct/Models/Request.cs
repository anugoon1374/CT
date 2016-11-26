// ***********************************************************************
// Author           : Anugoon Leelaphattarakij
// ***********************************************************************
// <copyright file="ServiceRequest.cs" company="CT">
//     Copyright (c) Anugoon Leelaphattarakij. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Beyond.Data;

namespace Beyond.Ct.Models
{
    /// <summary>
    /// Data model represents a Service Request.
    /// <para>A Service Request is a user request for information or advice, or for a standard change (a pre-approved change that is low risk, relatively common and follows a procedure) or for access to an IT service. A great example of a standard request is a password reset. Service Requests are usually handled by the Service Desk and do not require an RFC (Request for Change) to be submitted.</para>
    /// </summary>
    /// <remarks>
    /// This data model is designed following ITILv3 standard.
    /// </remarks>
    /// <seealso href="https://www.sunviewsoftware.com/learn/blog/3-reasons-to-separate-your-service-request-incident-change">ITILv3 Service Requests</seealso>
    public class ServiceRequest : Resource
    {
        #region entity properties

        [Required]
        public string Content { get; set; }

        #endregion

        #region related entities

        /// <summary>
        /// Gets or sets the <see cref="Tag" /> objects, that serve as a additional context to the <see cref="ServiceRequest"/>.
        /// </summary>
        /// <value>The <see cref="ICollection{Tag}" /> that serve as a additional context of the <see cref="ServiceRequest"/>.</value>
        public virtual ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="PersonUser" /> related to this instance.
        /// </summary>
        /// <value>The <see cref="ICollection{PersonUser}" />.</value>
        public virtual ICollection<PersonUser> Recipients { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="PersonUser" /> related to this instance.
        /// </summary>
        /// <value>The <see cref="PersonUser" />.</value>
        public virtual PersonUser Requester { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="PersonUser" /> identifier related to this instance.
        /// </summary>
        /// <value>The <see cref="PersonUser" /> identifier.</value>
        public long RequesterGuid { get; set; }


        #endregion

        #region constructor and disposable handler

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRequest"/> class.
        /// </summary>
        public ServiceRequest()
        {
        }

        #endregion constructor and disposable handler
    }
}
