using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.Controls
{
   public class CtrlSelectModel : CtrlBaseModel
    {
        public string Type { get; set; }
        public string Label { get; set; }

        public string InitialValue { get; set; }
        public string ColumnDataName { get; set; }

        public string Options { get; set; }


        public String CreateOptions(string a)
        {
            string[] names = a.Split(new char[] { ',' });
            var html = "";
            foreach (var option in names)
            {
                html += "<option value'"+option+"'>" + option+ "</option>";
            }

            return html;
        }

        public override string GetHtml()
        {
            var html= "" +
                "<div class='form-group row'>" +
                    "<label for= '{1}' class='form-label mt-4'>{0}</label>" +
                    "<div class='col-sm-10'> " +
                        "<select class='form-control'  id='{1}' value='{2}'  ColumnDataName='{3}'>" +
                            "{4}" +
                        "</select>" +
                    "</div>" +
                "</div>";


            return String.Format(html, Label, Id,InitialValue, ColumnDataName,CreateOptions(Options));
        }
    }
}//Laber Id