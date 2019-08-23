using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApolloPayslipDist.Models
{
    public partial class Account
    {
        public Guid AccountId { get; set; }
        public string AccountEmail { get; set; }
        [DataType(DataType.Password)]
        public string AccountPassword { get; set; }
        public string AccountSalt { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
