using Syncfusion.XForms.ComboBox;
using Syncfusion.XForms.DataForm;
using Syncfusion.XForms.DataForm.Editors;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataFormXamarin
{
    public class CustomDropDownEditor : DataFormDropDownEditor
    {
        DataFormDropDownItem dropDownItem;
        SfDataForm sfDataForm;
        public CustomDropDownEditor(SfDataForm dataForm) : base(dataForm)
        {
            this.sfDataForm = dataForm;
        }
        protected override void OnInitializeView(DataFormItem dataFormItem, SfComboBox view)
        {
            base.OnInitializeView(dataFormItem, view);
            view.IsEditableMode = true;
            view.Unfocused += View_Unfocused; 
            dropDownItem = dataFormItem as DataFormDropDownItem;
        }
        private void View_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var dropDown = sender as SfComboBox;
            var text = dropDown.Text;
            if (string.IsNullOrEmpty(text))
                return;
            var selectedItem = dropDown.SelectedItem;
            if (selectedItem == null || (selectedItem as Details)?.Name?.ToString() != text)
            {
                dropDownItem.ItemsSource.Add(new Details { Name = text });
                (DataForm.DataObject as Address).Country = text;
                sfDataForm.UpdateEditor("Country");
            }
        }
        protected override void OnUnWireEvents(SfComboBox view)
        {
            base.OnUnWireEvents(view);
            view.Unfocused -= View_Unfocused;
        }
    }
}