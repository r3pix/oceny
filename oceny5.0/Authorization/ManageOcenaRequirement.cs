using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace oceny5._0.Authorization
{

    public enum Operation
    {
        Create,
        Read,
        Delete,
        Update
    }

    public class ManageOcenaRequirement : IAuthorizationRequirement
    {
        public Operation Operation { get; set; }

        public ManageOcenaRequirement(Operation operation)
        {
            Operation = operation;
        }
    }
}
