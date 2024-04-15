using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebUI.Models.Controls
{
    public class CtrlTableModel : CtrlBaseModel
    {
        public CtrlTableModel()
        {
            Columns = "";
        }
        
        public string Title { get; set; }
        public string Columns { get; set; }
        public string ColumnsDataName { get; set; }
        public string FunctionName { get; set; }

        public int ColumnsCount => Columns.Split(',').Length;

        public string ColumnHeaders
        {
            get
            {
                var headers = "";
                foreach(var text in Columns.Split(','))
                {
                    headers += "<th>" + text + "</th>";
                }

                return headers;
            }
        }

        public override string GetHtml()
        {
            var html = ReadFileText();

            //Va por cada una de las propiedades de la clase
            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop != null)
                {
                    //Obtiene el valor de la propiedad
                    var value = prop.GetValue(this, null).ToString();

                    //tag = -#Title- --> "Listado de clientes"
                    var tag = string.Format("-#{0}-", prop.Name);
                    html = html.Replace(tag, value);
                }
            }

            return html;
        }


    

    }
}