namespace WcfTestService
{
    using System;

    /// <summary>
    /// The test service.
    /// </summary>
    public class TestService : ITestService
    {
        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetData(int value)
        {
            return $"You entered: {value}";
        }

        /// <summary>
        /// The get data using data contract.
        /// </summary>
        /// <param name="composite">
        /// The composite.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeType"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when composite is null
        /// </exception>
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException(nameof(composite));
            }

            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }

            return composite;
        }
    }
}
