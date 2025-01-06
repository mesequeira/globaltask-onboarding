using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.Users.Errors
{
    public static class UserErrorCodes
    {
        public static readonly string RequiredEmail = "User.NoEmailProvided";
        
        public static readonly string InvalidEmailFormat = "User.InvalidEmail";
        
        public static readonly string RequiredPhoneNumber = "User.NoPhoneNumberProvided";
        
        public static readonly string InvalidPhoneNumber = "User.InvalidPhoneNumber";
        
        public static readonly string PhoneNumberTooLong = "User.PhoneNumberTooLong";

        public static readonly string PhoneNumberTooShort = "User.PhoneNumberTooShort";
        
        public static readonly string RequiredPassword = "User.NoPasswordProvided";

        public static readonly string InvalidPassword = "User.InvalidPassword";

        public static readonly string RequiredUserName = "User.NoUserNameProvided";

        public static readonly string UserNameTooLong = "User.UserNameTooLong";

        public static readonly string InvalidBirthday = "User.InvalidBirthday";

        public static readonly string NotFound = "User.NotFound";

        public static readonly string SortByPropertyNotFound = "User.SortByPropertyNotFound";
    }
}
