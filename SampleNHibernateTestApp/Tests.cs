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
            _newCustomer.FName = "Billy";
            _newCustomer.LastName = "Stack";
            _newCustomer.CreationAttribute = new ModificationAttribute("BILLY", DateTime.Now.AddDays(-2.0));

            this.c_repository.SaveCustomer(_newCustomer);

            Assert.NotNull(this.c_repository.GetById(_newCustomer.TheId));
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
            this.c_insertedCustomerId = this.c_repository.GetAll()[0].TheId;

            var _expectedCustomer = new SampleNHibernateTestApp.Customer();
            _expectedCustomer.TheId = this.c_insertedCustomerId;
            _expectedCustomer.FName = "Billy";
            _expectedCustomer.LastName = "Stack";
            _expectedCustomer.CreationAttribute = new SampleNHibernateTestApp.ModificationAttribute("BILLY", DateTime.UtcNow);

            var _actualCustomer = this.c_repository.GetById(c_insertedCustomerId);

            Assert.NotNull(_actualCustomer);
            Assert.Equal(_expectedCustomer.TheId, _actualCustomer.TheId);
            Assert.Equal(_expectedCustomer.FName, _actualCustomer.FName);
            Assert.Equal(_expectedCustomer.LastName, _actualCustomer.LastName);
        }


        [Fact]
        public void GetByIdAndLatestDate_Good()
        {
            this.c_insertedCustomerId = this.c_repository.GetAll()[0].TheId;

            var _expectedCustomer = new SampleNHibernateTestApp.Customer();
            _expectedCustomer.TheId = this.c_insertedCustomerId;
            _expectedCustomer.FName = "Billy";
            _expectedCustomer.LastName = "Stack";
            _expectedCustomer.CreationAttribute = new SampleNHibernateTestApp.ModificationAttribute("BILLY", DateTime.UtcNow);

            var _actualCustomer = this.c_repository.GetById(c_insertedCustomerId);

            Assert.NotNull(_actualCustomer);
            Assert.Equal(_expectedCustomer.TheId, _actualCustomer.TheId);
            Assert.Equal(_expectedCustomer.FName, _actualCustomer.FName);
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