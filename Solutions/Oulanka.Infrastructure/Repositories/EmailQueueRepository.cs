using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class EmailQueueRepository : NHibernateRepository<EmailQueueItem> , IEmailQueueRepository
    {
        public IList<EmailQueueItem> Dequeue()
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<EmailQueueItem>()
                .Add(Restrictions.IsNull("NextTryTime") || Restrictions.Ge("NextTryTime", DateTime.Now));

            return criteria.List<EmailQueueItem>();
        }

        public void QueueSendingFailure(EmailQueueItem itemToQueue, int failureInterval, int maxNumberOfTries)
        {
            int currentNumber = itemToQueue.NumberOfTries;
            currentNumber += 1;

            if (currentNumber <= maxNumberOfTries)
            {
                itemToQueue.NumberOfTries = currentNumber;
                itemToQueue.NextTryTime = DateTime.Now.AddMinutes(currentNumber * failureInterval);
                this.SaveOrUpdate(itemToQueue);
            }
            else
            {
                this.Delete(itemToQueue);
            }

        }
    }
}