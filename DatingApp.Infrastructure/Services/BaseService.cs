using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Services
{
    public abstract class BaseService
    {
        public T MapSingleRowModel<T>(DataTable table, T model)
        {
            foreach (var prop in model.GetType().GetProperties(BindingFlags.DeclaredOnly |
                                                               BindingFlags.Public | BindingFlags.Instance))
            {
                if (table.Columns.Contains(prop.Name))
                {
                    prop.SetValue(model, table.Rows[0][prop.Name]);
                }
            }
            return model;
        }

        public T MapSingleRowModel<T>(DataRow dataRow, T model)
        {
            foreach (var prop in model.GetType().GetProperties(BindingFlags.DeclaredOnly |
                                                              BindingFlags.Public | BindingFlags.Instance))
            {
                object currentRowValue = dataRow[prop.Name];
                if (prop.PropertyType.Name != dataRow[prop.Name].GetType().Name)
                {
                    Type modelPropertyType = Type.GetType(prop.PropertyType.FullName);
                    currentRowValue = Convert.ChangeType(currentRowValue, modelPropertyType);
                }

                prop.SetValue(model, currentRowValue);
            }
            return model;
        }
    }
}
