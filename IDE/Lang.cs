using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE
{
    public class Lang
    {
        public string SelectedLang = "PL";

        public void CheckLanguage()
        {
            try
            {
                SelectedLang = File.ReadAllText("Language.txt");
            }
            catch(Exception Ex)
            {
            }
        }
    }
}
