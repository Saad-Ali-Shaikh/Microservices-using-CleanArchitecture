namespace Catalog.Core.Specs
{
    public class Pagination<T> where T : class
    {
        public Pagination()
        {

        }
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> dataList)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            DataList = dataList;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int Count { get; }
        public IReadOnlyList<T> DataList { get; }
    }
}
