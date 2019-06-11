using System;
using System.Text;
using NHibernate;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public static class RepositoryHelper
    {
        public static ISession GetSession()
        {
            var session = NHibernateSession.Current
                         ?? NHibernateSession.GetDefaultSessionFactory().OpenSession();

            return session;
        }



    }
}