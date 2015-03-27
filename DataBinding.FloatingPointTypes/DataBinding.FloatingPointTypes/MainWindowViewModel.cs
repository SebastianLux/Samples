using System;
using System.ComponentModel.DataAnnotations;

namespace DataBinding.FloatingPointTypes
{
    public class MainWindowViewModel : BindingBase
    {
        private Double? floatingPointTypeValue;

        [Required]
        [Range(-40.0, 100.0)]
        public Double? FloatingPointTypeValue
        {
            get { return floatingPointTypeValue; }
            set
            {
                floatingPointTypeValue = value;
                RaisePropertyChanged();
            }
        }
    }
}