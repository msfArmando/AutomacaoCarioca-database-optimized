using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoCarioca
{
    public class NodePaths
    {
        public static string Numero = "//nff:ListaNfse/nff:CompNfse/nff:Nfse/nff:InfNfse/nff:Numero";
        public static string CodigoVerificacao = "//nff:ListaNfse/nff:CompNfse/nff:Nfse/nff:InfNfse/nff:CodigoVerificacao";
        public static string DataEmissao = "//nff:ListaNfse/nff:CompNfse/nff:Nfse/nff:InfNfse/nff:DataEmissao";
        public static string ValorServico = "//nff:ListaNfse/nff:CompNfse/nff:Nfse/nff:InfNfse/nff:Servico/nff:Valores/nff:ValorServicos";
        public static string CnpjPrestador = "//nff:ListaNfse/nff:CompNfse/nff:Nfse/nff:InfNfse/nff:PrestadorServico/nff:IdentificacaoPrestador/nff:Cnpj";
        public static string RazaoSocialPrestador = "//nff:ListaNfse/nff:CompNfse/nff:Nfse/nff:InfNfse/nff:PrestadorServico/nff:RazaoSocial";
        public static string CnpjTomador = "//nff:ListaNfse/nff:CompNfse/nff:Nfse/nff:InfNfse/nff:TomadorServico/nff:IdentificacaoTomador/nff:CpfCnpj/nff:Cnpj";
        public static string CpfTomador = "//nff:ListaNfse/nff:CompNfse/nff:Nfse/nff:InfNfse/nff:TomadorServico/nff:IdentificacaoTomador/nff:CpfCnpj/nff:Cpf";
        public static string RazaoSocialTomador = "//nff:ListaNfse/nff:CompNfse/nff:Nfse/nff:InfNfse/nff:TomadorServico/nff:RazaoSocial";
    }
}
