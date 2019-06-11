namespace Oulanka.Api.Models
{
    public class GenericResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public object ReturnValue { get; set; }

        public static GenericResult Ok(string message)
        {
            return Generate(true, message, null);
        }

        public static GenericResult Ok(string message, object returnValue)
        {
            return Generate(true, message, returnValue);
        }

        public static GenericResult Failure(string message)
        {
            return Generate(false, message, null);
        }

        private static GenericResult Generate(bool isSucceded, string message, object returnValue)
        {
            var result = new GenericResult
            {
                Succeeded = isSucceded,
                Message = message,
                ReturnValue = returnValue
            };

            return result;
        }
    }
}