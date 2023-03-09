using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutomacaoCarioca
{
    public class AutomationForms
    {
        public static string Subscribe { get; set; }
        public static string Code { get; set; }
        public static string NumNota { get; set; }
        public static string GetForms()
        {
            IWebElement viewState = HelperPath.ValidationToReturnElement(PathsToAutomations.ByViewState, 1, 5);
            var viewStateValue = viewState.GetAttribute("value");
            viewStateValue = Uri.EscapeDataString(viewStateValue);

            IWebElement viewStateGenerator = HelperPath.ValidationToReturnElement(PathsToAutomations.ByViewStateGenerator, 1, 5);
            var viewStateGeneratorValue = viewStateGenerator.GetAttribute("value");
            viewStateGeneratorValue = Uri.EscapeDataString(viewStateGeneratorValue);

            IWebElement eventValidation = HelperPath.ValidationToReturnElement(PathsToAutomations.ByEventValidation, 1, 5);
            var eventValidationValue = eventValidation.GetAttribute("value");
            eventValidationValue = Uri.EscapeDataString(eventValidationValue);

            IWebElement linkOfXml = HelperPath.ValidationToReturnElement(PathsToAutomations.ByLinkXmlRequest, 1, 5);
            var linkOfXmlValue = linkOfXml.GetAttribute("value");

            var matchSub = Regex.Match(linkOfXmlValue, "inscricao=(.*)&nf");
            Subscribe = matchSub.Groups[1].Value;

            var matchCode = Regex.Match(linkOfXmlValue, "cod=(.*)");
            Code = matchCode.Groups[1].Value;

            return $"__VIEWSTATE={viewStateValue}&__VIEWSTATEGENERATOR={viewStateGeneratorValue}&__EVENTVALIDATION={eventValidationValue}&ctl00%24cphBase%24lkbWSNacional=&ctl00%24cphBase%24txtLinkCopiar=https%3A%2F%2Fnotacarioca.rio.gov.br%2Fnfse.aspx%3Finscricao%3D{Subscribe}%26nf%3D343%26cod%3D{Code}";
        }
    }
}
