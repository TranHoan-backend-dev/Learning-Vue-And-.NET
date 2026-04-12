namespace MISA.Common.Enum;

public static class AppEnum
{
    public enum StatusCode: int
    {
        Success = 200,
        BadRequest = 400,
        Created = 201,
        NoContent = 204,
        NotFound = 404,
        Unauthorized = 401,
        Forbidden = 403,
        InternalServerError = 500,
    }

    public enum ModelState : int
    {
        None = 0,
        Insert = 1,
        Update = 2,
        Delete = 3
    }
}