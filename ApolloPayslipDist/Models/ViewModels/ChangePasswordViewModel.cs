﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApolloPayslipDist.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Key]
        [Display(AutoGenerateField = false)]
        public Guid AccountId { get; set; }
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
