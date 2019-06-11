using System;
using System.Collections.Generic;
using Oulanka.Domain;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class EmailQueueService : IEmailQueueService
    {
        private readonly IEmailQueueRepository _queueRepository;

        public EmailQueueService(IEmailQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public ActionConfirmation Delete(int itemId)
        {
            var emailToDelete = _queueRepository.Get(itemId);

            if (emailToDelete != null)
            {
                try
                {
                    _queueRepository.Delete(emailToDelete);
                    _queueRepository.DbContext.CommitChanges();

                    return ActionConfirmation.CreateSuccess("The email was successfully deleted.");
                }
                catch
                {
                    return
                        ActionConfirmation.CreateFailure(
                            "A problem was encountered preventing the email from being deleted. " +
                            "Another item likely depends on this email");
                }
            }

            return
                ActionConfirmation.CreateFailure(
                    "The email could not be found to deletion. It may already have been deleted");
        }


        public IList<EmailQueueItem> Dequeue()
        {
            return _queueRepository.Dequeue();
        }

        public ActionConfirmation Queue(EmailQueueItem item)
        {
            try
            {
                if (item.IsValid())
                {

                    _queueRepository.SaveOrUpdate(item);
                    _queueRepository.DbContext.CommitChanges();

                    var saveOrUpdateConfirmation = ActionConfirmation.CreateSuccess("The emails were successfully queued.");
                    saveOrUpdateConfirmation.Value = item;

                    return saveOrUpdateConfirmation;

                }
                else
                {
                    _queueRepository.DbContext.RollbackTransaction();

                    return
                        ActionConfirmation.CreateFailure(
                            "The item could not be queued due to missing or invalid information");

                }

            }
            catch (Exception exception)
            {
                return
                    ActionConfirmation.CreateFailure(
                        "The email could not be saved due to missing or invalid information > " + exception.Message);
            }
        }


        public ActionConfirmation QueueSendingFailure(int itemId, int failureInterval, int maxNumberOfTries)
        {
            var emailToQueue = _queueRepository.Get(itemId);
            if (emailToQueue != null)
            {
                try
                {


                    _queueRepository.QueueSendingFailure(emailToQueue, failureInterval, maxNumberOfTries);

                    var confirmation =
                        ActionConfirmation.CreateSuccess(
                            "The email was successfully queued for sending failure");
                    confirmation.Value = emailToQueue;

                    return confirmation;
                }
                catch (Exception)
                {
                    return
                        ActionConfirmation.CreateFailure(
                            "A problem was encountered preventing the email queue sending failure");
                }
            }

            return
                ActionConfirmation.CreateFailure(
                    "The email could not be found to queue failure. It may already have been deleted");
        }
    }
}