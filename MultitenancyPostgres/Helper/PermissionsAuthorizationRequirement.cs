using Microsoft.AspNetCore.Authorization;
using MultitenancyPostgres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Helper
{
    public class PermissionsAuthorizationRequirement: IAuthorizationRequirement
    {
        public IEnumerable<Permissions> RequiredPermissions { get; }

        public PermissionsAuthorizationRequirement(IEnumerable<Permissions> requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }
    }
}
