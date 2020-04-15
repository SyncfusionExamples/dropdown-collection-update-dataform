using System;
using System.Collections.Generic;
using System.Text;

namespace DataFormXamarin
{
    public class ViewModelClass
    {
        public Address ContactInfo
        {
            get; set;
        }
        public ViewModelClass()
        {
            this.ContactInfo = new Address();
        }
    }
}
