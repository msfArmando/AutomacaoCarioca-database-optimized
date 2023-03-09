using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V108.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cookie = System.Net.Cookie;
using MongoDB.Driver;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using MongoDB.Driver.Core.Operations;
using OpenQA.Selenium.DevTools.V109.Network;
using System.Web;
using System.Collections.Specialized;
using System.Security.Principal;
using MongoDB.Bson.Serialization.Serializers;

namespace AutomacaoCarioca
{
    public class Automations : IDisposable
    {
        private static string _user { get; set; }
        private static string _password { get; set; }
        private static string _xmlString { get; set; }
        private static string _randomUserID => Guid.NewGuid().ToString();
        public static IWebDriver Driver => WebDriver.Driver;

        private static ConnectToDB _dataBase = new ConnectToDB();

        public void GoToWebSite(string url)
        {
            Driver.Navigate().GoToUrl(url);
            Driver.Manage().Window.Maximize();
            _dataBase.CreateLoginsCollection();
            _dataBase.CreateClientsCollection();
            _dataBase.CreateXmlCollection();
        }

        public void Login(string user, string password)
        {
            IWebElement inputUser = HelperPath.ValidationToReturnElement(PathsToAutomations.ByUserInput, 1, 10);
            inputUser.SendKeys(user);
            _user = user;

            IWebElement inputPassword = HelperPath.ValidationToReturnElement(PathsToAutomations.ByPasswordInput, 1, 10);    
            inputPassword.SendKeys(password);
            _password = password;

            IWebElement buttonLogin = HelperPath.ValidationToReturnElement(PathsToAutomations.ByBtnLogin, 1, 10);
            buttonLogin.Click();

            try
            {
                bool isElementDisplayed = Driver.FindElement(By.XPath(PathsToAutomations.ByLabelError)).Enabled;
            }
            catch (Exception ex)
            {
                XmlLogin xmlLogin = new XmlLogin(_user, _password);
                _dataBase.InsertLoginInDataBase(xmlLogin);
                throw;
            }
        }

        public void AcessXmlGroup()
        {
            IWebElement dropDownConsultas = HelperPath.ValidationToReturnElement(PathsToAutomations.ByMenuNotas, 1, 10);
            dropDownConsultas.Click();
        }

        public bool SelectDates(string month, string year)
        {
            string dateChoice = $"{month.Substring(0, 3)}/{year}";

            IWebElement dropDownDates = HelperPath.ValidationToReturnElement(PathsToAutomations.ByDropDownDates, 1, 10);

            var rows = dropDownDates.FindElements(By.TagName("option"));
            for (int index = 0; index < rows.Count; index++)
            {
                if (rows[index].Text == dateChoice)
                {
                    rows[index].Click();
                    return false;
                }
            }
            Console.WriteLine("Data não encontrada!");
            return true;
        }

        public void SelectClients()
        {
            var tableClients = HelperPath.ValidationToReturnElement(PathsToAutomations.ByClientsTable, 1, 10);
            var rowsOfTableclients = tableClients.FindElements(By.TagName("option"));
            for (int startIndex = 2; startIndex < rowsOfTableclients.Count; startIndex++)
            {
                IWebElement client = HelperPath.ValidationToReturnElement($"//*[@id=\"ctl00_cphCabMenu_ddlContribuinte\"]/option[{startIndex}]", 1, 10);
                client.Click();
                Driver.FindElement(By.XPath(PathsToAutomations.ByConsultButton)).Click();
                XmlClient xmlClient = new XmlClient();
                var inscricaoClient = Driver.FindElement(By.XPath($"//*[@id=\"ctl00_cphCabMenu_ctrlFiltros_ctrlInscricoes_ddlInscricoes\"]/option[{startIndex - 1}]"));
                xmlClient.InscricaoClient = inscricaoClient.GetAttribute("value");
                xmlClient.NameClient = inscricaoClient.Text;
                _dataBase.InsertClientsInDataBase(xmlClient);
                SelectXml();
                Driver.Navigate().Back();
            }
        }

        public static void SelectXml()
        {
            try
            {
                if (Driver.FindElement(By.XPath(PathsToAutomations.BySelectError)).Enabled)
                    return;
            }
            catch (Exception)
            {
                string clientId = Guid.NewGuid().ToString();
                var tableXml = HelperPath.ValidationToReturnElement(PathsToAutomations.ByXmlsTable, 1, 10);
                var rowsOfTableXml = tableXml.FindElements(By.TagName("tr"));
                for (int startIndex = 2; startIndex < rowsOfTableXml.Count; startIndex++)
                {
                    Driver.SwitchTo().Window(Driver.WindowHandles[0]);
                    IWebElement xml = HelperPath.ValidationToReturnElement($"//*[@id=\"ctl00_cphCabMenu_gvNotas\"]/tbody/tr[{startIndex}]/td[1]/a", 1, 10);

                    AutomationForms.NumNota = xml.Text.TrimStart('0');

                    xml.Click();

                    Driver.SwitchTo().Window(Driver.WindowHandles[1]);
                    IWebElement generateXml = HelperPath.ValidationToReturnElement(PathsToAutomations.ByGenerateXml, 1, 10);
                    //generateXml.Click();

                    SendPostRequest();
                    _dataBase.InsertXmlInDataBase(ReadXml());
                }
            }
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            Driver.Navigate().Back();
        }

        public static void SendPostRequest()
        {
            CookieContainer cookieContainer = AutomationCookies.GetCookies();

            string xmlForm = AutomationForms.GetForms();

            byte[] xmlFormBytes = Encoding.ASCII.GetBytes(xmlForm);

            try
            {
                HttpWebRequest requestWeb = (HttpWebRequest)HttpWebRequest.Create($"https://notacarioca.rio.gov.br/contribuinte/notaprint.aspx?inscricao={AutomationForms.Subscribe}&nf={AutomationForms.NumNota}&verificacao={AutomationForms.Code}");
                requestWeb.Method = "POST";
                requestWeb.ProtocolVersion = new Version(1, 1);
                requestWeb.Host = "notacarioca.rio.gov.br";
                requestWeb.KeepAlive = true;
                requestWeb.ContentLength = xmlFormBytes.LongLength;
                requestWeb.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
                requestWeb.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"110\", \"Not A(Brand\";v=\"24\", \"Google Chrome\";v=\"110\"");
                requestWeb.Headers.Add("sec-ch-ua-mobile", "?0");
                requestWeb.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
                requestWeb.Headers.Add("Upgrade-Insecure-Requests", "1");
                requestWeb.Headers.Add("Origin", "https://notacarioca.rio.gov.br");
                requestWeb.ContentType = "application/x-www-form-urlencoded";
                requestWeb.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36");
                requestWeb.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
                requestWeb.Headers.Add("Sec-Fetch-Site", "same-origin");
                requestWeb.Headers.Add("Sec-Fetch-Mode", "navigate");
                requestWeb.Headers.Add("Sec-Fetch-User", "?1");
                requestWeb.Headers.Add("Sec-Fetch-Dest", "document");
                requestWeb.Referer = $"https://notacarioca.rio.gov.br/contribuinte/notaprint.aspx?inscricao={AutomationForms.Subscribe}&nf=343&verificacao={AutomationForms.Code}";
                requestWeb.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                requestWeb.Headers.Add(HttpRequestHeader.AcceptLanguage, "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                requestWeb.CookieContainer = cookieContainer;
                requestWeb.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (var stream = requestWeb.GetRequestStream())
                {
                    stream.Write(xmlFormBytes, 0, xmlFormBytes.Length);
                }

                WebResponse response = requestWeb.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    _xmlString = reader.ReadToEnd();
                }
            }
            catch (Exception)   
            {
                throw;
            }
            WebDriver.Driver.Close();
        }

        public static XmlInfos ReadXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(_xmlString);

            XmlNamespaceManager nsm = new XmlNamespaceManager(xmlDoc.NameTable);
            nsm.AddNamespace("nff", "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd");

            XmlNode nodeNumero = xmlDoc.SelectSingleNode(NodePaths.Numero, nsm);
            string numero = nodeNumero.InnerText;

            XmlNode nodeCodigoVerificacao = xmlDoc.SelectSingleNode(NodePaths.CodigoVerificacao, nsm);
            string codigoVerificacao = nodeCodigoVerificacao.InnerText;

            XmlNode nodeDataEmissao = xmlDoc.SelectSingleNode(NodePaths.DataEmissao, nsm);
            string dataEmissao = nodeDataEmissao.InnerText;

            XmlNode nodeValorServico = xmlDoc.SelectSingleNode(NodePaths.ValorServico, nsm);
            string valorServico = nodeValorServico.InnerText;

            XmlNode nodeCnpjPrestador = xmlDoc.SelectSingleNode(NodePaths.CnpjPrestador, nsm);
            string cnpjPrestador = nodeCnpjPrestador.InnerText;

            XmlNode nodeRazaoSocialPrestador = xmlDoc.SelectSingleNode(NodePaths.RazaoSocialPrestador, nsm);
            string razaoSocialPrestador = nodeRazaoSocialPrestador.InnerText;

            string cpfcnpjTomador = string.Empty;
            try
            {
                XmlNode nodeCnpjTomador = xmlDoc.SelectSingleNode(NodePaths.CnpjTomador, nsm);
                cpfcnpjTomador = nodeCnpjTomador.InnerText;
            }
            catch (Exception)
            {
                XmlNode nodeCpfTomador = xmlDoc.SelectSingleNode(NodePaths.CpfTomador, nsm);
                cpfcnpjTomador = nodeCpfTomador.InnerText;
            }

            XmlNode nodeRazaoSocialTomador = xmlDoc.SelectSingleNode(NodePaths.RazaoSocialTomador, nsm);
            string razaoSocialTomador = nodeRazaoSocialTomador.InnerText;

            XmlInfos xmlInfos = new XmlInfos(numero, codigoVerificacao, dataEmissao, valorServico, cnpjPrestador, razaoSocialPrestador, cpfcnpjTomador, razaoSocialTomador);

            return xmlInfos;
        }

        public void Dispose()
        {
            Driver.Dispose();
            Driver.Quit();
        }
    }
}
