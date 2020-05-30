using mobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileAPI.BussinessLogic
{
    public class AuthentocationHelper
    {

        public AppUser ValidateUser(AppUser appUser)
        {
            using(MobileContext context=new MobileContext())
            {
                var _user= context.AppUser
                        .Where(u => u.UserName == appUser.UserName
                                && u.Password == appUser.Password
                        ).FirstOrDefault();

            

                return _user;
            }
        }

        public List<TblForm> GetScreenNamesForRole(string roleId,out string errorMessage)
        {
            try
            {
                errorMessage = string.Empty;
                using (MobileContext context = new MobileContext())
                {
                    var _permittedScreens = context.TblFormPermission.Where(f => f.RoleId == Convert.ToInt32(roleId)).FirstOrDefault();
                    if (_permittedScreens !=null)
                    {
                        var screenCodes = _permittedScreens.FormId.Split(",").ToList();
                        return context.TblForm.Where(frm => screenCodes.Contains(frm.FormCode)).ToList();
                    }

                    errorMessage = "No Screen Configured for User";
                    return null;

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
