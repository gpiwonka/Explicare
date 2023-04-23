using Microsoft.VisualStudio.OLE.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Explicare.Settings;
using Newtonsoft.Json;
using System.Windows.Controls.Primitives;

namespace Explicare.OpenAIClient
{
    public class OAIClient
    {
        private ToolkitPackage Package;


        public OAIClient(ToolkitPackage package)
        {
            Package = package;

        }


        private HttpClient SetupClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "OpenAI-DotNet");

            SettingsPage page = (SettingsPage)Package.GetDialogPage(typeof(SettingsPage));




            if (String.IsNullOrWhiteSpace(page.OPENAI_KEY))

            {
                throw new InvalidCredentialException($"{page.OPENAI_KEY} is empty");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", page.OPENAI_KEY);


            if (!string.IsNullOrWhiteSpace(page.OPENAI_ORGANIZATION_ID))
            {
                client.DefaultRequestHeaders.Add("OpenAI-Organization", page.OPENAI_ORGANIZATION_ID);
            }

            return client;
        }


        public async Task<String> ExplainAsync(String code)
        {
            string explained = "";
            OpenAIModel request = new OpenAIModel();
            request.prompt = code + " explain code ";



            var jsonContent = JsonConvert.SerializeObject(request);

            using (var client = SetupClient())
            {

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                var response = client.PostAsync("https://api.openai.com/v1/completions", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<OpenAIResponse>(responseStr);
                    explained = result.choices[0].text;
                }
                else
                {
                    throw new BadRequestExecption();
                }

            }

            return explained;
        }



    }
}
