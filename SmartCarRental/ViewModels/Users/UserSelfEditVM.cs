﻿using SmartCarRental.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartCarRental.ViewModels.Users
{
    public class UserSelfEditVM : UserEditVM
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

    }
}
