using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Explicare.OpenAIClient;
using System.Linq;

namespace Explicare
{

    public partial class ExplicareToolWindowControl : UserControl
    {
        DTE MyDTE;
        ToolkitPackage ToolkitPackage;

        public ExplicareToolWindowControl()
        {
            InitializeComponent();
        }

        public ExplicareToolWindowControl(DTE dTE,ToolkitPackage toolkitPackage)
        {
            InitializeComponent();
            MyDTE = dTE;
            ToolkitPackage = toolkitPackage;    
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            string TextToExplain = "";
            string text = "";
            try
            {
                var textSelection = (TextSelection)MyDTE.ActiveDocument.Selection;

                if (String.IsNullOrEmpty(textSelection.Text))
                {
                    TextDocument doc = (TextDocument)(MyDTE.ActiveDocument.Object("TextDocument"));

                    var p = doc.StartPoint.CreateEditPoint();
                    TextToExplain = p.GetText(doc.EndPoint);

                }
                else
                {
                    TextToExplain = textSelection.Text;

                }
                textBlock.Text = CodeExcerpt(TextToExplain) + Environment.NewLine;

                OAIClient client = new OAIClient(ToolkitPackage);
                text = await client.ExplainAsync(TextToExplain);
            }
            catch (BadRequestExecption bre)
            {
                text = "Something went wrong!" + Environment.NewLine + " Please check the API Key!";

            }
            catch (Exception ex)
            {

                text = "Sorry! Something went wrong!";
            }
            textBlock.Text +=  text;
        }


        public static async Task<object> PostCallAPI(string url, object jsonObject)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<object>(jsonString);
                    }
                }
            }
            catch (Exception ex)
            {
                //myCustomLogger.LogException(ex);
            }
            return null;
        }

        public string CodeExcerpt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            string[] words = input.Split(' ');
            if (words.Length == 1 || input.Length <= 50)
            {
                return input;
            }

            int index = 0;
            int count = 0;

            while (index < words.Length && count + words[index].Length <= 50)
            {
                count += words[index].Length + 1;
                index++;
            }

            if (index == words.Length)
            {
                return input;
            }

            string result = string.Join(" ", words.Take(index));
            result = result.Substring(0, result.Length - 1);
            result += " " + words[index] + " ....";
            return result;
        }

    }
}