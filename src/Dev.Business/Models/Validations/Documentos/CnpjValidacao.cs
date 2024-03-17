namespace Dev.Business.Models.Validations.Documentos
{
    public class CnpjValidacao
    {
        public const int TamanhoCnpj = 14;

        public static bool Validar(string value)
        {
            var cnpjNumeros = Utils.ApenasNumeros(value);

            if (!TamanhoValido(cnpjNumeros)) return false;

            return !TemDigitosRepetidos(cnpjNumeros) && TemDigitosValidos(cnpjNumeros);
        }

        private static bool TamanhoValido(string value)
        {
            return value.Length == TamanhoCnpj;
        }

        private static bool TemDigitosRepetidos(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            return invalidNumbers.Contains(value);
        }

        private static bool TemDigitosValidos(string valor)
        {
            var number = valor.Substring(0, TamanhoCnpj - 2);

            var digitoVerificador = new DigitoVerificador(number)
                .ComMultiplicadoresDeAte(2, 9)
                .Substituindo("0", 10, 11);
            var firstDigit = digitoVerificador.CalculaDigito();
            digitoVerificador.AddDigito(firstDigit);
            var secondDigit = digitoVerificador.CalculaDigito();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(TamanhoCnpj - 2, 2);
        }
    }
}
