//using Syncfusion.Maui.DataForm;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataForm_DataValidation
//{
//    public class SignInFormModel
//    {
//        #region Constructor
//        public SignInFormModel()
//        {
//            this.UserName = string.Empty;
//            this.Password = string.Empty;
//        }

//        #endregion

//        #region Property
//        [Display(Name = "usernamein", Prompt = "example@mail.com")]
//        [EmailAddress(ErrorMessage = "Enter your email")]
//        [DataFormDisplayOptions(ValidMessage = "Valid mail")]
//        public string? UserName { get; set; }

//        [Display(Name = "passwordin", Prompt = "Enter your password")]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter the password")]
//        [DataType(DataType.Password)]
//        [MinLength(8, ErrorMessage = "Password length must be greater then 8 characters")]
//        [DataFormDisplayOptions(ValidMessage = "Password strength is good")]
//        public string? Password { get; set; }

//        #endregion       
//    }
//}
