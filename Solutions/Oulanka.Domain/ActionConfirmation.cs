namespace Oulanka.Domain
{
    public class ActionConfirmation
    {
        private ActionConfirmation() { }

        public string Message { get; set; }
        public object Value { get; set; }
        public bool WasSuccessful { get; private set; }

        /// <summary>
        /// Creates the failure.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static ActionConfirmation CreateFailure(string message)
        {
            return new ActionConfirmation {Message = message, WasSuccessful = false};
        }

        /// <summary>
        /// Creates the sucess.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static ActionConfirmation CreateSuccess(string message)
        {
            return new ActionConfirmation {Message = message, WasSuccessful = true};
        }

    }
}