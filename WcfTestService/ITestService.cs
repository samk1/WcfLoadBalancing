namespace WcfTestService
{
    using System.Runtime.Serialization;
    using System.ServiceModel;

    /// <summary>
    /// The TestService interface.
    /// </summary>
    [ServiceContract]
    public interface ITestService
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
        [OperationContract]
        string GetData(int value);

        /// <summary>
        /// The get data using data contract.
        /// </summary>
        /// <param name="composite">
        /// The composite.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeType"/>.
        /// </returns>
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

    /// <summary>
    /// The composite type.
    /// </summary>
    [DataContract]
    public class CompositeType
    {
        /// <summary>
        /// Gets or sets a value indicating whether bool value.
        /// </summary>
        [DataMember]
        public bool BoolValue { get; set; } = true;

        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        [DataMember]
        public string StringValue { get; set; } = "Hello ";
    }
}
