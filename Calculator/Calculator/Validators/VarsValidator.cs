using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace WpfApp2.Validators
{
    public static class VarsValidator
    {
        public static bool ValidateName(string name)
        {
            if (name == "") return false;
            string patternAlpha = @"^[a-zA-Z]+$";
            Regex regexAlpha = new Regex(patternAlpha);
            return regexAlpha.IsMatch(name);
        }

        public static bool ValidateValue(string value)
        {
            if (value == "") return false;
            string patternNumeric = @"^-?\d+(,\d+)*$";
            Regex regexAlpha = new Regex(patternNumeric);
            return regexAlpha.IsMatch(value);
        }
    }
}
