﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BanHang
{
    public class Common
    {
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }
            return new SelectList(list, "Value", "Text");
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                PropertyInfo[] props= typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
                foreach (PropertyInfo prop in props)
                {
                    dataTable.Columns.Add(prop.Name);
                }
                foreach(T item in items)
                {
                    var values = new object[props.Length];
                    for(int i=0;i<props.Length;i++)
                    {
                        values[i] = props[i].GetValue(item,null);
                    }
                    dataTable.Rows.Add(values);
                }
                return dataTable;
            }
        }

        public class ProductType
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}