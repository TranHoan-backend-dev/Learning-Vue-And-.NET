namespace MISA.BL.Enums;

public static class AppEnum
{
    public enum StatusCode
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
}