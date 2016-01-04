using System;
using System.Collections.Generic;
using System.Text;

namespace HubApp1.Common
{
    public class HelperClass
    {
        public enum PageMode
        {
            Add,
            Edit
        };
    }

    public class EditDriverObject
    {
        public HelperClass.PageMode pageMode { get; set; }
        public int Id { get; set; }
    }
}
