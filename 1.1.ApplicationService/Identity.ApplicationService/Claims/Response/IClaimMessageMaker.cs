namespace Identity.ApplicationService.Claims.Response
{
    public interface IClaimMessageMaker
    {
        string Message { get; }
        void SetMessage(ClaimCrudType crudType, HttpStatusCode status);
    }

    public enum ClaimCrudType : byte
    {
        add = 1,
        replace,
        delete,
        read
    }
}