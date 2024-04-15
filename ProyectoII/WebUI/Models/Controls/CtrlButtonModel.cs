using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.Controls
{
    public class CtrlButtonModel : CtrlBaseModel
    {
        public string Label { get; set; }
        public string Color { get; set; }

        public string Type { get; set; }

        public string FunctionName { get; set; }




        public override string GetHtml()
        {
            var html = ReadFileText();

            //Va por cada una de las propiedades de la clase
            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop != null && prop.GetValue(this, null)!=null)
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