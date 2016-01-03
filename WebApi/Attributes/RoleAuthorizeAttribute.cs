using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using QuickErrandsWebApi.Models;

namespace QuickErrandsWebApi.Attributes
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private IEnumerable<Type> GetAllTypes(Type type)
        {
            var currentBaseType = type.BaseType;

            while (currentBaseType != typeof(object))
            {
                yield return currentBaseType;

                currentBaseType = currentBaseType.BaseType;
            }
        }

        public RoleAuthorizeAttribute(Type roleType)
        {
            if (roleType == typeof(BaseRole))
                return;

            var types = GetAllTypes(roleType);

            Roles = string.Join(",", types.Select(p => p.Name).ToArray());
        }
    }
}