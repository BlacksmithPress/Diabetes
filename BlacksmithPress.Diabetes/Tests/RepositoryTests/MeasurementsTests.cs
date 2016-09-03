using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlacksmithPress.Diabetes.Entities;
using BlacksmithPress.Diabetes.Persistence.Repositories;
using BlacksmithPress.Diabetes.Types;
using NodaTime;
using NUnit.Framework;
using ObjectApproval;
using Shouldly;

namespace RepositoryTests
{
    public class MeasurementsTests : TestBase
    {
        public MeasurementsTests()
        {
            configuration = new Configuration();
            container = configuration.Container;
            measurements = new Measurements(container);
            people = new People(container);
        }

        private IConfiguration configuration;
        private IContainer container;
        private Measurements measurements;
        private People people;

        private IPerson Ken => people.Get(1);
        private IClock Clock => SystemClock.Instance;

        [Test]
        public void MeasurementsRepository_Create_CreatesNewMeasurement()
        {
            // arrange isolation

            // arrange test
            var expected = new Measurement
            {
                Attribute = MeasuredAttribute.Glucose,
                Subject = Ken,
                Timestamp = Clock.Now,
                UnitOfMeasure = UnitOfMeasure.mgdL,
                Amount = 150M,
            };

            // act
            var actual =  measurements.Create(expected);

            // assert
            actual.Id.ShouldBeGreaterThan(0);
            ObjectApprover.VerifyWithJson(actual);

            // clean-up
            measurements.Delete(actual.Id);
        }


        [Test]
        public void MeasurementsRepository_AssignNewPersonToSubject_CreatesRelationshipNotEmbeddedPerson()
        {
            // arrange isolation
            DeletePeople("Karl Smirnoff");

            // arrange test

            // act
            var subject = new Person {Name = "Karl Smirnoff"};
            var measurement = new Measurement {Subject = subject};
            measurements.Create(measurement);
            var actual = people.GetAll().FirstOrDefault(p => p.Name == "Karl Smirnoff");

            // assert
            actual.ShouldNotBeNull();
            actual.Id.ShouldBeGreaterThan(0);


            // clean-up
            DeletePeople("Karl Smirnoff");
        }

        private void DeletePeople(string name)
        {
            var persons = people.GetAll().Where(p => p.Name == name);
            foreach (var person in persons)
            {
                foreach (var measurement in measurements.GetAll().Where(m => m.Subject.Id == person.Id))
                {
                    measurements.Delete(measurement.Id);
                }
                people.Delete(person.Id);
            }
        }
    }
}
