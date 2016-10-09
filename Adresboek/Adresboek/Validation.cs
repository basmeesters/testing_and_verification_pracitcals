using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adresboek
{
    using Chars = IEnumerable<char>;

    static public class Validation
    {

        static public bool ValidateEmail(string email)
        {
            // if there is no at-sign, the string is not valid
            if (!email.Contains('@'))
                return false;

            // split string in local and domain part
            int atIndex = email.LastIndexOf('@');
            string localpart = email.Substring(0, atIndex);
            string domainpart = email.Substring(atIndex + 1);

            // validate the local part
            if (!validateLocalPart(localpart))
                return false;

            // validate the domain part
            if (!validateDomainPart(domainpart))
                return false;

            // both parts are valid and thus this string is valid 
            return true;
        }

        static public bool ValidateName(string name)
        {
            // an empty string is in valid format
            if (isEmpty(name))
                return true;

            // check if the string is in valid dot-format, otherwise return false
            if (!hasValidSingleDots(name))
                return false;

            // check if string exists of valid characters
            Chars allowedChars = AllowNameChars();
            return name.Select(c => allowedChars.Contains(c)).All(b => (bool)b);
        }



        //----
        // validation helpers
        //----

        // validate the localpart of the emaialdress
        static public bool validateLocalPart(string str)
        {
            // an empty string is in valid format
            if (isEmpty(str))
                return true;

            // check if there are valid quote pairs, otherwise return false
            List<int> quoteLocations = locateQuotes(str);
            if (quoteLocations.Count() % 2 != 0)
                return false;

            // check if quotes are valid, otherwise return false
            List<string> quotes = quotesFromLocations(quoteLocations, str);
            if (!quotes.Select(validateLocalQuotePart).All(b => (bool)b))
                return false;

            // remove quote-parts because they already validated
            str = removeQuotes(quoteLocations, str);

            // check if the remaining string is in valid dot-format, otherwise return false
            if (!hasValidDots(str))
                return false;

            // check if the remaining string exists of valid characters
            Chars allowedChars = AllowedLocalpartChars();
            return str.Select(c => isValidChar(c, allowedChars)).All(b => (bool)b);
        }

        // validate the quoted string in the localpart of a emaialddress
        static public bool validateLocalQuotePart(string str)
        {
            // an empty string is in valid format
            if (isEmpty(str))
                return true;

            // check if these characters are prefixed with a slash
            if (!hasPrefixes(str, "\\\"", '\\'))
                return false;

            // check if string exists of valid characters
            Chars allowedChars = AllowedLocalpartQuoteChars();
            return str.Select(c => isValidChar(c, allowedChars)).All(b => (bool)b);
        }

        // validate the domain part of the emailaddress
        static public bool validateDomainPart(string str)
        {
            // an empty string is in valid format
            if (isEmpty(str))
                return true;

            // check if string is in valid dot-format, otherwise return false
            if (!hasValidDots(str))
                return false;

            // check if string exists of valid characters
            Chars allowedChars = AllowedDomainPartChars();
            return str.Select(c => isValidChar(c, allowedChars)).All(b => (bool)b);
        }


        //----
        // operations on quotes in a emailaddress
        //----

        // get positions of not-escaped quote characters in a string
        static List<int> locateQuotes(string str)
        {
            List<int> locations = new List<int>();
            for (int i = 0; i < str.Length; i++)
                if (str[i] == '"' && (i == 0 || str[i - 1] != '\\'))
                    locations.Add(i);
            return locations;
        }

        // get the text between quotes, given a string and the locations of quote characters
        static List<string> quotesFromLocations(List<int> locations, string str)
        {
            List<string> quotes = new List<string>();
            for (int i = 0; i < locations.Count() - 1; i = i + 2)
                quotes.Add(str.Substring(locations[i] + 1, locations[i + 1] - locations[i] - 1));
            return quotes;
        }

        // remove the quotes text from a string
        static string removeQuotes(List<int> locations, string str)
        {
            for (int i = 0; i < locations.Count() - 1; i = i + 2)
                str = str.Remove(locations[i] + 1, locations[i + 1] - locations[i] - 1);
            return str;
        }


        //----
        // primitive validation functions
        //----

        // check if the given string is empty
        static bool isEmpty(string str)
        {
            return str.Length == 0;
        }

        // check if a character is part of the valid character set
        static bool isValidChar(char c, Chars validChars)
        {
            return validChars.Contains(c);
        }

        // check if all specified characters in the string are prefixed with the prefix-character
        // this is required in the quoted part of the emailadress for the character \ en " (backslash and quote).
        static bool hasPrefixes(string str, string prefixedChars, char prefix)
        {
            for (int i = 0; i < str.Length; i++)
            {
                // if current character is a prefix character then the next character must be a character that needs to be escaped
                if (str[i] == prefix)
                {
                    if (i + 1 == str.Length || !prefixedChars.Contains(str[++i]))
                        return false;
                }
                // if the current character is a character that must be escaped then it is not be prefixed (invariant of this loop).
                else if (prefixedChars.Contains(str[i]))
                    return false;
            }
            return true;
        }

        // check if the string is correct with respect to the dot constraints
        static public bool hasValidDots(string str)
        {
            // an empty string contains no dots, thus allowed
            if (isEmpty(str))
                return true;

            // a dot as first or last character isn't allowed
            if (str[0] == '.' || str[str.Length - 1] == '.')
                return false;

            // multiple dots are not allowed
            return hasValidSingleDots(str);
        }

        // check if the string is correct with respect to the single-dot constraints
        static bool hasValidSingleDots(string str)
        {
            // multiple dots are not allowed
            return !str.Contains("..");
        }


        //----
        // define allowed char-sets
        //----

        // generate a character set from a specific range
        static Chars CharsInRange(char start, char end)
        {
            return Enumerable.Range(start, end - start + 1).Select(x => (char)x);
        }

        static Chars AllowAlpha()
        {
            Chars allow_az = CharsInRange('a', 'z');
            Chars allow_AZ = CharsInRange('A', 'Z');
            return allow_az.Concat(allow_AZ);
        }

        static Chars AllowAlphaNumeric()
        {
            Chars allow_alpha = AllowAlpha();
            Chars allow_09 = CharsInRange('0', '9');
            return allow_alpha.Concat(allow_09);
        }

        // return a character set that describes the valid characters of a name
        static Chars AllowNameChars()
        {
            Chars allow_alphanumeric = AllowAlpha();
            Chars allow_with_constraint = (Chars)". ";
            return allow_alphanumeric.Concat(allow_with_constraint);
        }

        // return a character set that describes the valid characters in the Localpart of a emailaddress
        static Chars AllowedLocalpartChars()
        {
            Chars allow_alphanumeric = AllowAlphaNumeric();
            Chars allow_specials = (Chars)"!#$%&’*+-/=?^_‘{|}~";
            Chars allow_with_constraint = (Chars)".\"";
            return allow_alphanumeric.Concat(allow_specials).Concat(allow_with_constraint);
        }

        // return a chracter set that describe the valid characters in a quoted part of the emailaddress
        static Chars AllowedLocalpartQuoteChars()
        {
            Chars allow_localpart = AllowedLocalpartChars();
            Chars allow_specials = (Chars)" \\(),:;<>@[]";
            return allow_localpart.Concat(allow_specials);
        }

        // return a character set that describes the valid characters in the domain part of the emailaddress
        static Chars AllowedDomainPartChars()
        {
            Chars allow_alphanumeric = AllowAlphaNumeric();
            Chars allow_with_constraints = (Chars)".-";
            return allow_alphanumeric.Concat(allow_with_constraints);
        }
    }
}
