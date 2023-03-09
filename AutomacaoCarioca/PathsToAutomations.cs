using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoCarioca
{
    public class PathsToAutomations
    {
        public static string ByUserInput => "//*[@id=\"ctl00_cphCabMenu_tbCpfCnpj\"]";
        public static string ByPasswordInput => "//*[@id=\"ctl00_cphCabMenu_tbSenha\"]";
        public static string ByBtnLogin => "//*[@id=\"ctl00_cphCabMenu_btEntrar\"]";
        public static string ByLabelError => "//*[@id=\"ctl00_cphCabMenu_vsErros\"]";
        public static string ByMenuNotas => "//*[@id=\"ctl00_Vcab_mnuRotinasn5\"]/td/table/tbody/tr/td[1]/a";
        public static string ByDropDownDates => "//*[@id=\"ctl00_cphCabMenu_ddlCompetenciaEmitidas\"]";
        public static string ByClientsTable => "//*[@id=\"ctl00_cphCabMenu_ddlContribuinte\"]";
        public static string ByConsultButton => "//*[@id=\"ctl00_cphCabMenu_btConsultarNFEmitida\"]";
        public static string ByXmlsTable => "//*[@id=\"ctl00_cphCabMenu_gvNotas\"]/tbody";
        public static string ByGenerateXml => "//*[@id=\"ctl00_cphBase_lkbWSNacional\"]";
        public static string ByViewState => "//*[@id=\"__VIEWSTATE\"]";
        public static string ByViewStateGenerator => "//*[@id=\"__VIEWSTATEGENERATOR\"]";
        public static string ByEventValidation => "//*[@id=\"__EVENTVALIDATION\"]";
        public static string ByLinkXmlRequest => "//*[@id=\"ctl00_cphBase_txtLinkCopiar\"]";
        public static string BySelectError => "//*[@id=\"ctl00_cphCabMenu_gvNotas_ctl01_lblErro\"]";
    }
}
