using System.ComponentModel.DataAnnotations;

namespace BlacksmithPress.Diabetes.Types
{
    /// <summary>
    /// An enumeration identifying various attributes that can be measured.
    /// </summary>
    public enum MeasuredAttribute
    {
        /// <summary>
        /// The level of glucose in the subject's blood
        /// </summary>
        Glucose,
    }
    /// <summary>
    /// An enumeration identifying various units of measure.
    /// </summary>
    public enum UnitOfMeasure
    {
        /// <summary>
        /// Milligrams per deciliter (mg/dL) unit of measure
        /// </summary>
        [Display(Description = "mg/dL")]
        mgdL,
        /// <summary>
        /// Millimoles per liter (mmol/L) unit of measure
        /// </summary>
        [Display(Description = "mmol/L")]
        mmolL,
    }

}