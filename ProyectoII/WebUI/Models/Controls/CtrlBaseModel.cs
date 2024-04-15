using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebUI.Models.Controls
{ 
    public abstract class CtrlBaseModel 
    {
        public string Id { get; set; }
        public string ViewName { get; set; }

        public abstract string GetHtml();

        protected string ReadFileText()
        {
            string fileName = this.GetType().Name + ".html";

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + Path.Combine(@"Models\Controls", fileName);

            string text = File.ReadAllText(path);

            return text;
        }

    }
}