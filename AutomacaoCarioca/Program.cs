using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomacaoCarioca
{
    class Program
    {
        static void Main()
        {
            bool ciclo = true;
            bool userValid = false;
            bool passwordValid = false;
            var userLogin = string.Empty;
            var passwordLogin = string.Empty;
            bool cicloDate;

            Automations auto = new Automations();

            auto.GoToWebSite("https://notacarioca.rio.gov.br/senhaweb/login.aspx");

            while (ciclo)
            {
                if (userValid != true)
                {
                    Console.WriteLine("Digite seu CPF ou CNPJ: ");
                    userLogin = Console.ReadLine();
                    if(userLogin == "")
                    {
                        Console.WriteLine("O campo não pode ser nulo!");
                        continue;
                    }
                    else if(Regex.IsMatch(userLogin, @"^\d+$") == false)
                    {
                        Console.WriteLine("O campo não aceita letras!");
                        continue;
                    }
                }

                if (passwordValid != true)
                {
                    Console.WriteLine("Digite sua senha: ");
                    passwordLogin = Console.ReadLine();
                    if (passwordLogin == "")
                    {
                        Console.WriteLine("O campo não pode ser nulo!");
                        continue;
                    }
                }

                try
                {
                    auto.Login(userLogin, passwordLogin);
                    Console.WriteLine("Erro ao tentar realizar login. Tente novamente com as credenciais corretas!");
                    ciclo = true;
                }
                catch (Exception)
                {
                    ciclo = false;
                }
            }

            auto.AcessXmlGroup();

            do
            {
                Console.WriteLine("Digite o nome do mês que deseja buscar: (Exemplo: janeiro, fevereiro, março...)");
                string mes = Console.ReadLine().ToLower();
                Console.WriteLine("Digite o ano que deseja buscar: ");
                string ano = Console.ReadLine();

                cicloDate = auto.SelectDates(mes, ano);

            } while (cicloDate);

            auto.SelectClients();

            auto.Dispose();
        }
    }
}