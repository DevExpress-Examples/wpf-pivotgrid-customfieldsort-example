using DevExpress.Xpf.Editors;
using DevExpress.Xpf.PivotGrid;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Wpf_PivotGrid_CustomFieldSort_Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<MyOrderRow> OrderSourceList {get; set;}
        public MainWindow()
        {
            InitializeComponent();
            pivotGridControl1.CustomFieldSort += PivotGridControl1_CustomFieldSort;
            checkEdit1.Checked += CheckEdit1_Checked;
            checkEdit1.Unchecked += CheckEdit1_Unchecked;
        }

        private void CheckEdit1_Unchecked(object sender, RoutedEventArgs e)
        {
            // Set the default sorting algorithm.
            fieldSalesPerson.SortMode = FieldSortMode.Default;
        }

        private void CheckEdit1_Checked(object sender, RoutedEventArgs e)
        {
            // Enable the CustomFieldSort event for the fieldSalesPerson field.
            fieldSalesPerson.SortMode = FieldSortMode.Custom;
        }

        private void PivotGridControl1_CustomFieldSort(object sender, DevExpress.Xpf.PivotGrid.PivotCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "SalesPersonName")
            {
                int result;
                if (e.SortLocation == FieldSortLocation.Pivot)
                {
                    object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "SalesPersonId"),
                        orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "SalesPersonId");
                    result = Comparer.Default.Compare(orderValue1, orderValue2);
                }
                else
                {
                    // Compare last names.
                    result = Comparer.Default.Compare(e.Value1.ToString().Split(' ')[1], e.Value2.ToString().Split(' ')[1]);
                    // If last names are the same, compare first names.
                    if (result == 0)
                        result = Comparer.Default.Compare(e.Value1.ToString().Split(' ')[0], e.Value2.ToString().Split(' ')[0]);
                }
                e.Result = result;
                e.Handled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OrderSourceList = DatabaseHelper.CreateData();
            pivotGridControl1.DataSource = OrderSourceList;
            pivotGridControl1.BestFitArea = DevExpress.Xpf.PivotGrid.FieldBestFitArea.FieldHeader;
            pivotGridControl1.BestFit();
        }
    }
}
