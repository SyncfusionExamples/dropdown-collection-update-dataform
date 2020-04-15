using Syncfusion.XForms.DataForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataFormXamarin
{
    public class SourceProviderContactForm : SourceProvider
    {
        public override IList GetSource(string sourceName)
        {
            List<Details> details = new List<Details>();
            if (sourceName == "Country")
            {
                details.Add(new Details { Name = "Canada", Code = 1 });
                details.Add(new Details { Name = "USA", Code = 2 });
                details.Add(new Details { Name = "India", Code = 3 });
            }
            return details;
        }
    }
}
