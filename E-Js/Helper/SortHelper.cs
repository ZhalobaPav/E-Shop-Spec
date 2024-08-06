using ApplicationCore.Enities;
using ApplicationCore.Extensions;
using Shared.Enums;

namespace E_Js.Helper
{
    public class SortHelper<T>
    {
        public static List<T> Sort(List<T> entities, SortParam sort)
        {
            string orderBy = sort?.SortBy;
            bool ifOrderAsc = sort != null ? sort.SortDirection == SortDirection.Ascending : true;

            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = nameof(BaseEntity.CreatedDateUtc);
            }

            return ifOrderAsc
                ? entities.OrderByExtension(orderBy).ToList()
                : entities.OrderByDescending(orderBy).ToList();
        }
    }
}
