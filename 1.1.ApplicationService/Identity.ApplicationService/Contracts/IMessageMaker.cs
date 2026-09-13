using System.Net;

namespace Identity.ApplicationService.Contracts
{
    public interface IMessageMaker
    {
        string Message { get; }

        void SetMessage(Crud crud, HttpStatusCode status);
    }

    public enum Crud : byte
    {
        create = 1,
        update,
        delete,
        read,
        find
    }
}
