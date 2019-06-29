using ModernStore.Domain.ValueObjects;
using ModernStore.Shared.Entities;
using System;

namespace ModernStore.Domain.Entities
{
    public class Customer : Entity
    {
        protected Customer() { }

        public Customer(Name name, Email email, Document document, User user)
        {
            Name = name;
            BirthDate = null;
            Email = email;
            Document = document;
            User = user;

            AddNotifications(name, email, document);
        }

        public Name Name { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public Email Email { get; private set; }
        public Document Document { get; private set; }
        public User User { get; private set; }

        public void Update(Name name, DateTime birthDate)
        {
            AddNotifications(name);

            Name = name;
            BirthDate = birthDate;
        }
    }
}
