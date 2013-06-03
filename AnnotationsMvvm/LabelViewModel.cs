using System;
using System.ComponentModel;

namespace AnnotationsMvvm
{
    /// <summary>
    /// Defines the ViewModel for a single label. 
    /// </summary>
    public class LabelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LabelViewModel(IComparable x1, IComparable y1, string text, string tooltip)
        {
            X1 = x1;
            Y1 = y1;
            LabelText = text;
            LabelToolTip = tooltip;
        }

        /// <summary>
        /// LabelText will allow us to bind to TextAnnotation.Text
        /// </summary>
        public string LabelText { get; set; }

        /// <summary>
        /// LabelTooltip will be used to add a custom tooltip to the TextAnnotation
        /// </summary>
        public string LabelToolTip { get; set; }

        /// <summary>
        /// X1 defines the X Data-Value for positioning the annotation
        /// </summary>
        public IComparable X1 { get; set; }

        /// <summary>
        /// Y1 defines the Y Data-Value for positioning the annotation
        /// </summary>
        public IComparable Y1 { get; set; }
    }
}
