using System.Collections.Generic;

namespace QuickErrandsWebApi.BindingModels
{
    public class UpdateUserModel
    {
        public string UserName { get; set; }

        public List<string> Roles { get; set; }
    }
}