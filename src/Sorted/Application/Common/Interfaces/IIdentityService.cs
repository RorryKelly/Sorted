public interface IIdentityService
{
    Guid GetUserId();
    bool HasAccess(string parentId, string userId);
}