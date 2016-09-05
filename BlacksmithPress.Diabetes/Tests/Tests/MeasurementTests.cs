using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlacksmithPress.Diabetes.Cloud.Controllers;
using BlacksmithPress.Diabetes.Persistence.Database;
using BlacksmithPress.Diabetes.Types;
using NodaTime;
using NUnit.Framework;
using ObjectApproval;
using Shouldly;

namespace Tests
{
    public class MeasurementTests
    {


        [Test]
        public void MeasurementsController_Post_CreatesMeasurement()
        {
            // arrange isolation
            var id = default(long);
            using (var context = new Context("BlacksmithPress.Diabetes"))
            {
                id = context.Measurements.Any() ? context.Measurements.Max(m => m.Id) + 1 : 1;
            }

            // arrange test
            var controller = new MeasurementsController();
            var expected = new Measurement
            {
                Timestamp = SystemClock.Instance.Now,
                Subject = new Person { Id = 2, Name = "Ethan"},
                Attribute = MeasuredAttribute.Glucose,
                Amount = 150M,
                UnitOfMeasure = UnitOfMeasure.mgdL,
            };

            // act
            var result = controller.PostMeasurement(expected);

            // assert
            using (var context = new Context("BlacksmithPress.Diabetes"))
            {
                var actual = context.Measurements.Find(id);
                expected.Id = id;
                actual.ShouldBe(expected);
            }

            // clean-up
        }


    }
}
