﻿using System.ComponentModel.DataAnnotations;

namespace BlacksmithPress.Diabetes.Data
{
    public enum MeasuredAttribute
    {
        Glucose,
    }
    public enum UnitOfMeasure
    {
        [Display(Description = "mg/dL")]
        mgdL,
        [Display(Description = "mmol/L")]
        mmolL,
    }

}