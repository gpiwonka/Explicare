using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Explicare.Settings
{
    [ComVisible(true)]
    public class SettingsPage : DialogPage
    {
        private String openAI_Key = "";
        private string openAI_ORGANIZATION_ID = "";
        

        [Category("Explicare")]
        [DisplayName("API key")]
        [Description("Your OPENAI Secret key")]
        public String OPENAI_KEY
        {
            get { return openAI_Key; }
            set { openAI_Key = value; }
        }

        [Category("Explicare")]
        [DisplayName("Organization ID")]
        [Description("Identifier for this organization sometimes used in API requests")]
        public string OPENAI_ORGANIZATION_ID
        {
            get { return openAI_ORGANIZATION_ID; }
            set { openAI_ORGANIZATION_ID = value; }
        }




    }
}
 