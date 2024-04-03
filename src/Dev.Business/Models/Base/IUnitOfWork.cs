namespace Dev.Business.Models.Base
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
