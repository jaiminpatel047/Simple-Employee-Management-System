using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleCurd.Model
{
    public class Table
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Search? search { get; set; }
        public List<Order>? order { get; set; }
        public List<Column>? columns { get; set; }
        public bool? IsActive { get; set; }
        public string SortColumnName
        {
            get
            {
                if (order != null && order.Count > 0 && columns != null && columns.Count > 0)
                {
                    var columnIndex = order[0].column;
                    return columns[order[0].column].data;
                }
                return null;
            }
        }
        public string SortDirection
        {
            get
            {
                if (order != null && order.Count > 0)
                {
                    return order[0].dir;
                }
                return null;
            }
        }
    }
    public class Search
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }
    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
    public class Column
    {
        public string? data { get; set; } // Property name
        public string? name { get; set; } // Column name for sorting/filtering
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search? search { get; set; }
    }
    public class DataTableResult<T>
    {
        public int totalRecords { get; set; }
        public int filteredrecords { get; set; }
        public IEnumerable<T> data { get; set; }
    }
}
