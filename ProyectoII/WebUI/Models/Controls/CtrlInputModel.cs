using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.Controls
{
   public class CtrlInputModel : CtrlBaseModel
    {
        public string Type { get; set; }
        public string Label { get; set; }
        public string PlaceHolder { get; set; }

        public string InitialValue { get; set; }
        public string ColumnDataName { get; set; }

        public string InputCLass { get; set; }

        public override string GetHtml()
        {
            var html= "" +
                "<div class='form-group row'>" +
                    "<label for= '{2}' class='form-label mt-4'>{0}</label>" +
                    "<div class='col-sm-10'> " +
                        "<input type = '{5}' placeholder='{1}' class='{6}'  id='{2}' value='{3}'  ColumnDataName='{4}'>" +
                    "</div>" +
                "</div>";


            return String.Format(html, Label, PlaceHolder, Id, InitialValue ,ColumnDataName, Type, InputCLass);
        }
    }
}