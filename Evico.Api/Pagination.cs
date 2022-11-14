namespace Evico.Api;

public class Pagination
{
    public Pagination(int itemsPerPage)
    {
        ItemsPerPage = itemsPerPage;
    }

    public int ItemsPerPage { get; }

    public int GetOffset(int totalCount)
    {
        return totalCount * ItemsPerPage;
    }
}