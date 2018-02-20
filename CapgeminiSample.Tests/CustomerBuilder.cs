using CapgeminiSample.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapgeminiSample.Tests
{
    class CustomerBuilder
    {
        private string Surname { get; set; } = "Thumbro";
        private string Name { get; set; } = "of Glimmerworld";

        private CustomerBuilder()
        {
        }

        public CustomerBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public CustomerBuilder WithSurname(string surname)
        {
            Surname = surname;
            return this;
        }

        public CustomerBuilder DefaultCustomer
        {
            get
            {
                return new CustomerBuilder();
            }
        }
        public Customer Build()
        {
            return new Customer()
            {
                Name = Name,
                Surname = Surname,
            };
        }
    }
}
