using AppLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.Controls;

namespace WebUI.Models.Helpers
{
    public static class ControlExtensions
    {
        public static HtmlString CtrlButton(this HtmlHelper html, 
            string viewName, 
            string id, 
            string label,             
            string onClickFunction = "", 
            string buttonType = "primary")
        {
            var ctrl = new CtrlButtonModel()
            {
                Id = id,
                Label = label,                
                ViewName = viewName,
                Type=buttonType,
                FunctionName=onClickFunction,
            };

            return new HtmlString(ctrl.GetHtml());
                
        }

        public static HtmlString CtrlInput(this HtmlHelper html,
            string id, string type,
            string label, string placeHolder = "",
            string columnDataName = "")
        {
            var ctrl = new CtrlInputModel
            {
                Id = id,
                Type = type,
                Label = label,
                PlaceHolder = placeHolder,
                ColumnDataName = columnDataName,
                InputCLass = "form-control"
            };

            return new HtmlString(ctrl.GetHtml());
        }

        // 
        public static HtmlString CtrlInput(this HtmlHelper html,string id, string type, string classes, string label, string placeHolder = "",string columnDataName = "")
        {
            var ctrl = new CtrlInputModel
            {
                Id = id,
                Type = type,
                Label = label,
                PlaceHolder = placeHolder,
                ColumnDataName = columnDataName,
                InputCLass = "form-control " + classes
            };

            return new HtmlString(ctrl.GetHtml());
        }


        public static HtmlString CtrlDatePicker(this HtmlHelper html,
            string id, string type,
            string label, string placeHolder = "",
            string columnDataName = "")
        {
            var ctrl = new CtrlDatePickerModel
            {
                Id = id,
                Type = type,
                Label = label,
                PlaceHolder = placeHolder,
                ColumnDataName = columnDataName
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlSelect(this HtmlHelper html,
            string id, string type,
            string label,
            string columnDataName = "", String options = "")
        {
            var ctrl = new CtrlSelectModel
            {
                Id = id,
                Type = type,
                Label = label,
                ColumnDataName = columnDataName,
                Options = options
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlTable(this HtmlHelper html, string viewName, string id, string title,
            string columnsTitle, string ColumnsDataName, string onSelectFunction)
        {
            var ctrl = new CtrlTableModel
            {
                ViewName = viewName,
                Id = id,
                Title = title,
                Columns = columnsTitle,
                ColumnsDataName = ColumnsDataName,
                FunctionName = onSelectFunction
            };
            
            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlPaypalBtn(this HtmlHelper html, string viewName)
        {
            FeeManager fm = new FeeManager();
            var fee = fm.retrieveFee().ToString();
            var ctrl = new PaypalBtnModel
            {
                Amount = fee,
            };

            return new HtmlString(ctrl.GetHtml());
        }
    }
}