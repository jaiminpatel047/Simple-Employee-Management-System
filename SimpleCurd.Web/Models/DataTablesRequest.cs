using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SimpleCurd.Web.Models
{
    public class TableDTO
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public SearchDTO Search { get; set; }
        public List<OrderDTO> Order { get; set; }
        public List<ColumnDTO> Columns { get; set; }
        public string SortColumnName
        {
            get
            {
                if (Order != null && Order.Count > 0 && Columns != null && Columns.Count > 0)
                {
                    var columnIndex = Order[0].Column;
                    return Columns[columnIndex].Name;
                }
                return null;
            }
        }
        public string SortDirection
        {
            get
            {
                if (Order != null && Order.Count > 0)
                {
                    return Order[0].Dir;
                }
                return null;
            }
        }
    }
    public class SearchDTO
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }
    public class OrderDTO
    {
        public int Column { get; set; }
        public string Dir { get; set; } // "asc" or "desc"
    }
    public class ColumnDTO
    {
        public string Data { get; set; } // Property name
        public string Name { get; set; } // Column name for sorting/filtering
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public SearchDTO Search { get; set; }
    }
}
