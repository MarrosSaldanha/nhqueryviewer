using System;
using Xunit;
using System.Linq;


namespace SampleNHibernateTestApp
{
    /// <summary>
    /// Tests class
    /// bstack @ 08/09/2009
    /// </summary>
    public class Tests
    {
        private SampleNHibernateTestApp.Repository c_repository;
        private Guid c_insertedCustomerId;


        public Tests()
        {
            this.c_repository = new SampleNHibernateTestApp.Repository();
        }


        [Fact]
        public void Save_Good()
        {
            var _newCustomer = new Customer();
            _newCustomer.FirstName = "Billy";
            _newCustomer.LastName = "Stack";

            this.c_repository.SaveCustomer(_newCustomer);

            Assert.NotNull(this.c_repository.GetById(_newCustomer.Id));
        }


        [Fact]
        public void GetAll_Good()
        {
            var _allCustomers = this.c_repository.GetAll();

            Assert.NotNull(_allCustomers);
            Assert.True(_allCustomers.Count > 0);
        }


        [Fact]
        public void GetById_Good()
        {
            this.c_insertedCustomerId = this.c_repository.GetAll()[0].Id;

            var _expectedCustomer = new SampleNHibernateTestApp.Customer();
            _expectedCustomer.Id = this.c_insertedCustomerId;
            _expectedCustomer.FirstName = "Billy";
            _expectedCustomer.LastName = "Stack";

            var _actualCustomer = this.c_repository.GetById(c_insertedCustomerId);

            Assert.NotNull(_actualCustomer);
            Assert.Equal(_expectedCustomer.Id, _actualCustomer.Id);
            Assert.Equal(_expectedCustomer.FirstName, _actualCustomer.FirstName);
            Assert.Equal(_expectedCustomer.LastName, _actualCustomer.LastName);
        }


        [Fact]
        public void GetWhereAgeGreaterThan20()
        {
            var _actualCustomer = this.c_repository.GetByGreaterThanAge(20);

            Assert.NotNull(_actualCustomer);
        }
    }
}