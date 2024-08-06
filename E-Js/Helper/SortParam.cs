using Shared.Enums;

namespace E_Js.Helper
{
    public class SortParam
    {
        public string SortBy { get; set; } = null;
        public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
        public SortParam()
        {
            
        }
        public SortParam(string sortBy, byte? sortDirection)
        {
            SortBy = sortBy;
            if(sortDirection == null) 
            {
                SortDirection = SortDirection.Ascending;
            }
            else if (Enum.IsDefined(typeof(SortDirection), sortDirection))
            {
                SortDirection = (SortDirection)sortDirection;
            }
            else
            {
                SortDirection= SortDirection.Ascending;
            }
        }
    }
}
