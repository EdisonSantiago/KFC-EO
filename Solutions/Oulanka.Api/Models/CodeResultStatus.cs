namespace Oulanka.Api.Models
{
    public class CodeResultStatus
    {
        public int Status { get; }
        public string Message { get; }

        public CodeResultStatus(int status)
        {
            if (status == 401)
                Message = "Unauthorized access. Login required";

            Status = status;
        }

        public CodeResultStatus(int code, string message)
        {
            Status = code;
            Message = message;
        }
    }
}