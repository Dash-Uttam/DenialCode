using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace DanialProject.Models
{
	public class DataTableModelBinder : IModelBinder
	{
		public DataTableModelBinder()
		{
		}

		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			if (bindingContext.ModelType != typeof(DataTableRequest))
			{
				return false;
			}
			DataTableRequest model = (DataTableRequest)bindingContext.Model ?? new DataTableRequest();
			model.Draw = Convert.ToInt32(this.GetValue(bindingContext, "draw"));
			model.Start = Convert.ToInt32(this.GetValue(bindingContext, "start"));
			model.Length = Convert.ToInt32(this.GetValue(bindingContext, "length"));
			model.Search = new DataTableSearch()
			{
				Value = this.GetValue(bindingContext, "search.value"),
				Regex = Convert.ToBoolean(this.GetValue(bindingContext, "search.regex"))
			};
			int o = 0;
			List<DataTableOrder> order = new List<DataTableOrder>();
			while (this.GetValue(bindingContext, string.Concat("order[", o, "].column")) != null)
			{
				order.Add(new DataTableOrder()
				{
					Column = Convert.ToInt32(this.GetValue(bindingContext, string.Concat("order[", o, "].column"))),
					Dir = this.GetValue(bindingContext, string.Concat("order[", o, "].dir"))
				});
				o++;
			}
			model.Order = order.ToArray();
			int c = 0;
			List<DataTableColumn> columns = new List<DataTableColumn>();
			while (this.GetValue(bindingContext, string.Concat("columns[", c, "].data")) != null)
			{
				columns.Add(new DataTableColumn()
				{
					Data = this.GetValue(bindingContext, string.Concat("columns[", c, "].data")),
					Name = this.GetValue(bindingContext, string.Concat("columns[", c, "].name")),
					Orderable = Convert.ToBoolean(this.GetValue(bindingContext, string.Concat("columns[", c, "].orderable"))),
					Searchable = Convert.ToBoolean(this.GetValue(bindingContext, string.Concat("columns[", c, "].searchable"))),
					Search = new DataTableSearch()
					{
						Value = this.GetValue(bindingContext, string.Concat("columns[", c, "][search].value")),
						Regex = Convert.ToBoolean(this.GetValue(bindingContext, string.Concat("columns[", c, "].search.regex")))
					}
				});
				c++;
			}
			model.Columns = columns.ToArray();
			bindingContext.Model = model;
			return true;
		}

		private string GetValue(ModelBindingContext context, string key)
		{
			ValueProviderResult result = context.ValueProvider.GetValue(key);
			if (result == null)
			{
				return null;
			}
			return result.AttemptedValue;
		}
	}
}