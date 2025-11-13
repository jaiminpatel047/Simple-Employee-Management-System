using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Helper
{
    internal class CommentHelper
    {
        internal static string CreateUniqueId(string firstName, DateTime dateOfBirth, string email, string phone)
        {
            string SafeSubstring(string input, int length) =>
                string.IsNullOrWhiteSpace(input)
                    ? string.Empty
                    : input.Substring(0, Math.Min(length, input.Length)).ToUpper();
            
            string dobPart = dateOfBirth.ToString("dd"); 
            string firstNamePart = SafeSubstring(firstName, 1); 
            string departmentPart = SafeSubstring(email, 1); 
            string rolePart = SafeSubstring(phone, 1); 
            string baseId = $"{dobPart}{firstNamePart}{departmentPart}{rolePart}";
            var random = new Random();
            while (baseId.Length < 6)
            {
                baseId += random.Next(0, 9).ToString();
            }
            return baseId.Substring(0, 6).ToUpper();
        }
    }
}
