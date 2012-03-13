namespace ReleaseManager.Tests
{
    using System;
    using NUnit.Framework;

    public abstract class BehaviorDrivenSpecificationBase
    {
        protected Exception ExceptionThrown { get; private set; }

        /// <summary>
        /// Method used to setup the model that will be tested.  This includes creating mock objects,
        /// preparing the model properties, preparing the database, etc.
        /// </summary>
        protected abstract void EstablishContext();

        /// <summary>
        /// Perform actions upon the model.  Separate test methods would then be employed to verify
        /// the results of the system under test.
        /// </summary>
        protected abstract void When();

        [SetUp]
        public void Setup()
        {
            EstablishContext();

            try
            {
                When();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} thrown from When method: {1}", exc.GetType().Name, exc.Message);
                ExceptionThrown = exc;
            }
        }
    }
}
