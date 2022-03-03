using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using HtmlString = Microsoft.AspNetCore.Html.HtmlString;

namespace LoocERP.Helpers
{



    public class Table
    {
        string id = "";


        public IHtmlContent print()
        {
            return new HtmlString("<strong>" + id + "</strong><script>alert('" + id + "')</script>");
        }

        public void addColumn(string name)
        {

        }
    }

    public static class TableExtension
    {
    }






















    public class DataTableHelper
    {

        string id = "";
        List<string> columns;



        public static DataTableHelper DataTable()
        {
            return new DataTableHelper();
        }


        public DataTableHelper setID(string id)
        {
            this.id = id;

            return this;
        }


        public DataTableHelper Columns(List<string> columns)
        {
            this.columns = columns;
            return this;
        }



        public static string Modal()
        {
            string ret = "";









            return ret;
        }


        public IHtmlContent print()
        {
            return new HtmlString("<strong>"+id+"</strong><script>alert('"+id+"')</script>");
        }


    }
}
