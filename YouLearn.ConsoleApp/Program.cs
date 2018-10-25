using System;
using YouLearn.Domain.Arguments.Usuario;
using YouLearn.Domain.Services;

namespace YouLearn.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("T E S T E S");

            AdicionarUsuarioRequest request = new AdicionarUsuarioRequest()
            {
                Email = "ricardo.freitas@gmail.com",
                PrimeiroNome = "Ricardo",
                UltimoNome = "Araújo",
                Senha = "12eeee"
            };

            var response = new ServiceUsuario().AdicionarUsuario(request);

            Console.ReadKey();
        }
    }
}
