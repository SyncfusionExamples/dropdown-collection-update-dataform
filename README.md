# How to add entered text to the dropdown collection in Xamarin.Forms DataForm (SfDataForm)

You can add the entered text to the DropDown editor’s collection in Xamarin.Forms [SfDataForm](https://help.syncfusion.com/xamarin/dataform/getting-started?) by using a custom editor.

You can create and add the custom editor to SfDataForm by overriding the [DataFormEditor](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfDataForm.XForms~Syncfusion.XForms.DataForm.Editors.DataFormEditor?) class, where the CustomDropDownEditor is inherited using DataFormDropDownEditor.

Refer to the [online user guide documentation](https://help.syncfusion.com/xamarin/sfdataform/editors?) for creating a new custom editor in DataForm.

You can also refer the following article.

https://www.syncfusion.com/kb/11402/how-to-add-entered-text-to-the-dropdown-collection-in-xamarin-forms-dataform-sfdataform

**C#**

DropDown editor modified by customizing DataFormDropDownEditor. When unfocused from dropdown entered text added to the collection.
``` c#
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
```
Refer to the following code example for binding [DataObject](https://help.syncfusion.com/xamarin-android/sfdataform/getting-started?) and register the editor using [RegisterEditor](https://help.syncfusion.com/cr/cref_files/xamarin-android/Syncfusion.SfDataForm.Android~Syncfusion.Android.DataForm.SfDataForm~RegisterEditor.html?) as CustomDropDownEditor to make data form items as a custom editor in DataForm.

**C#**

Customized DropDown editors registered to DataForm.
``` c#
public class DataFormBehavior : Behavior<ContentPage>
{
    SfDataForm dataForm;
    protected override void OnAttachedTo(ContentPage bindable)
    {
        base.OnAttachedTo(bindable);
        dataForm = bindable.FindByName<SfDataForm>("dataForm");
        dataForm.SourceProvider = new SourceProviderContactForm();
        dataForm.RegisterEditor("DropDown", new CustomDropDownEditor(dataForm));
        dataForm.RegisterEditor("Country", "DropDown");
        dataForm.AutoGeneratingDataFormItem += DataForm_AutoGeneratingDataFormItem;
    }
    private void DataForm_AutoGeneratingDataFormItem(object sender, AutoGeneratingDataFormItemEventArgs e)
    {
        if (e.DataFormItem != null)
        {
            if (e.DataFormItem.Name == "Country")
            {
                (e.DataFormItem as DataFormDropDownItem).DisplayMemberPath = "Name";
                (e.DataFormItem as DataFormDropDownItem).SelectedValuePath = "Name";
            }
        }
    }
    protected override void OnDetachingFrom(ContentPage bindable)
    {
        base.OnDetachingFrom(bindable);
        dataForm.AutoGeneratingDataFormItem -= DataForm_AutoGeneratingDataFormItem;
    }
}
```
**Output**

![DropDownCollectionUpdate](https://github.com/SyncfusionExamples/dropdown-collection-update-dataform/blob/master/ScreenShots/DropDownCollectionUpdate.gif)
