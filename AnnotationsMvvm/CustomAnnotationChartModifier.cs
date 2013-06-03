using Abt.Controls.SciChart;
using System.Collections;
using System.Windows;

namespace AnnotationsMvvm
{
    /// <summary>
    /// The CustomAnnotationChartModifiers provides a bridge between ViewModel and SciChartSurface, where we can 
    /// access the Labels (via data binding) as well as the SciChart API to add, remove and manipulate annotations
    /// </summary>
    public class CustomAnnotationChartModifier : ChartModifierBase
    {
        public static readonly DependencyProperty LabelsSourceProperty = DependencyProperty.Register("LabelsSource", typeof(IEnumerable), typeof(CustomAnnotationChartModifier), new PropertyMetadata(null, OnLabelsSourceChanged));

        // Here LabelsSource is IEnumerable, but you could easily make it ObservableCollection<LabelViewModel> 
        // in order to get changed notifications when items are added to, or removed from the collection
        public IEnumerable LabelsSource
        {
            get { return (IEnumerable) GetValue(LabelsSourceProperty); }
            set { SetValue(LabelsSourceProperty, value); }
        }

        // Get a notification when new labels are set.
        private static void OnLabelsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var modifier = (CustomAnnotationChartModifier) d;
            IEnumerable newValue = e.NewValue as IEnumerable;
            if (newValue == null) return;

            modifier.RebuildAnnotations();
        }

        // Recreate all annotations
        private void RebuildAnnotations()
        {
            if (base.ParentSurface == null || LabelsSource == null)
                return;

            // Take a look at the base class, ChartModifierBase for a wealth of API 
            // methods and properties to manipulate the SciChartSurface
            var annotationCollection = base.ParentSurface.Annotations;
            annotationCollection.Clear();

            foreach(var item in LabelsSource)
            {
                annotationCollection.Add(new CustomTextAnnotation() { DataContext = item });
            }
        }

        /// <summary>
        /// Called when the Chart Modifier is attached to the Chart Surface
        /// </summary>
        public override void OnAttached()
        {
            base.OnAttached();

            // Catch the condition where LabelsSource binds before chart is shown. Rebuild annotations
            if (LabelsSource != null && base.ParentSurface.Annotations.Count == 0)
            {
                RebuildAnnotations();
            }
        }
    }
}
