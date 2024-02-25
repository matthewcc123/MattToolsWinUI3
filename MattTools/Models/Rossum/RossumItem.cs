using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Models.Rossum
{
    public class RossumItem
    {
        public string Name { get; set; }
        public int AnnotationID { get; set; }
        public int DocumentID { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public string StatusFormarted { get { return ToUpperEveryWord(Status.Replace("_", " ")); } }

        public string ToUpperEveryWord(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var words = s.Split(' ');

            var t = "";
            foreach (var word in words)
            {
                t += char.ToUpper(word[0]) + word.Substring(1) + ' ';
            }
            return t.Trim();
        }

    }
}
