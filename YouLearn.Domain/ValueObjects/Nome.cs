using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace YouLearn.Domain.ValueObjects
{
    public class Nome: Notifiable
    {
        public Nome(string primeiroNome, string ultimoNome)
        {
            this.PrimeiroNome = primeiroNome;
            this.UltimoNome = ultimoNome;

            new AddNotifications<Nome>(this)
                .IfNullOrInvalidLength(x => x.PrimeiroNome, 1, 50, "Primerio Nome deve conter de 3 a 50 caracteres")
                .IfNullOrInvalidLength(x => x.UltimoNome, 1, 50, "Último Nome deve conter de 3 a 50 caracteres");
        }

        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }   
    }
}
