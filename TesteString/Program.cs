using System.Text.RegularExpressions;

string linkOfXmlValue = "https://notacarioca.rio.gov.br/nfse.aspx?inscricao=5816815&nf=343&cod=72M3B9TS";

var matchSub = Regex.Match(linkOfXmlValue, "inscricao=(.*)&nf");
string Sub = matchSub.Groups[1].Value;

var matchCod = Regex.Match(linkOfXmlValue, "cod=(.*)");
string Code = matchCod.Groups[1].Value;
Console.ReadLine();
