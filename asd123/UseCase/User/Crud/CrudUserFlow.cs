using asd123.Services;

namespace asd123.UseCase.User.Crud
{
    public class CrudUserFlow
    {
        private readonly IUnitOfWork uow;
        public CrudUserFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }
    }
}