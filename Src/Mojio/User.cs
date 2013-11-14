﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Mojio
{
    public partial class User : GuidEntity
    {
        /// <summary>
        /// username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// first name
        /// </summary>
        [Display(Name="First Name")]
        [MaxLength(32, ErrorMessage="Maximum lenght 32 characters")]
        public string FirstName { get; set; }
        
        /// <summary>
        /// last name
        /// </summary>
        [Display(Name = "Last Name")]
        [MaxLength(32, ErrorMessage = "Maximum lenght 32 characters")]
        public string LastName { get; set; }

        /// <summary>
        /// email address
        /// </summary>
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        /// <summary>
        /// ggb
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// optional number of credits
        /// </summary>
        public int? Credits { get; set; }

        /// <summary>
        /// record creation timestamp
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// most recent activity timestamp
        /// </summary>
        public DateTime LastActivityDate { get; set; }

        /// <summary>
        /// most recent login timestamp
        /// </summary>
        public DateTime LastLoginDate { get; set; }        
    }

    //$HarshKumar - 14/Nov/2013 - Added this enum - START
    public enum ImageSize
    {
        Small,
        Medium,
        Large
    }
   
    public class UserImage : GuidEntity
    {
        public string ImageType { get; set; }

        public ImageSize? ImageSize { get; set; }

        public byte[] Image { get; set; }
    }

   
    //$HarshKumar - 14/Nov/2013 - Added this enum - END
}
