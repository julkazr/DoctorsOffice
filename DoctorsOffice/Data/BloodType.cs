using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsOffice.Data
{
    public enum BloodType
    {
        A_PLUS,
        A_MINUS,
        AB_PLUS,
        AB_MINUS,
        B_PLUS,
        B_MINUS,
        O_PLUS,
        O_MINUS
    }

    public static class BloodTypeConversions
    {
        public static string ToPrettyFormat(this BloodType bloodType)
        {
            switch (bloodType)
            {
                case BloodType.A_PLUS: return "A+";
                case BloodType.A_MINUS: return "A-";
                case BloodType.AB_PLUS: return "AB+";
                case BloodType.AB_MINUS: return "AB-";
                case BloodType.B_PLUS: return "B+";
                case BloodType.B_MINUS: return "B-";
                case BloodType.O_PLUS: return "O+";
                case BloodType.O_MINUS: return "O-";
                default: return "";
            }
        }
    }

}