using Entities;
using Entities.MicroServices.Models;
using Microsoft.AspNetCore.Mvc;
using Services.ServiceInterfaces;

namespace Authentication.Api.Controllers;

[Route("Authentication")]
public class AuthenticationController(ITokenService tokenService):ControllerBase
{
    private const int AccessTokenExpireMinutes = 5;
    private const int RefreshTokenExpireMinutes = 180;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // Проверка логина и пароля пользователя
        if (ModelState.IsValid || !IsValidUser(model.Login, model.Password))
        {
            return Unauthorized(new ProblemDetails
                                {
                                    Title = "User data is not valid",
                                    Detail = "The provider user data is not valid",
                                    Instance = HttpContext.Request.Path
                                });
        }

        string accessToken;
        string refreshToken;

        try
        {
            // Создание access токена
            accessToken = tokenService.TryGenerateToken(AccessTokenExpireMinutes, model.Login, Role.User);

            // Создание refresh Token
            refreshToken = tokenService.TryGenerateToken(RefreshTokenExpireMinutes, model.Login, Role.User);

            // Добавление refresh токена в базу данных
            await tokenService.TryAddTokenToDataBaseAsync(refreshToken);
        }
        catch (Exception exception)
        {
            return Unauthorized(exception);
        }

        return Ok(new { accessToken, refreshToken });
    }

    [HttpPost("updateAccessToken")]
    public async Task<IActionResult> UpdateTokens([FromBody] RefreshTokensModel refreshTokensModel)
    {
        // Проверка валидности refresh токена
        if (!await tokenService.IsValidRefreshTokenAsync(refreshTokensModel.RefreshToken))
        {
            return Unauthorized(new ProblemDetails
                                {
                                    Title = "Invalid Refresh Token",
                                    Detail = "The provided refresh token is not valid.",
                                    Instance = HttpContext.Request.Path
                                });
        }

        string newAccessToken;
        string newRefreshToken;

        try
        {
            // Получение данных из токена
            var login = tokenService.TryGetLoginFromToken(refreshTokensModel.RefreshToken);
            var role = tokenService.TryGetRoleFromToken(refreshTokensModel.RefreshToken);

            // Создание нового access токена
            newAccessToken = tokenService.TryGenerateToken(AccessTokenExpireMinutes, login, role);

            // Создание нового refresh токена
            newRefreshToken = tokenService.TryGenerateToken(RefreshTokenExpireMinutes, login, role);

            // Обновление токена в базе данных
            await tokenService.TryUpdateTokenInDataBaseAsync(refreshTokensModel.RefreshToken, newRefreshToken);
        }
        catch (Exception)
        {
            return Unauthorized(new ProblemDetails
                                {
                                    Title = "Token Generation Error",
                                    Detail = "An error occurred while generating tokens.",
                                    Instance = HttpContext.Request.Path
                                });
        }

        return Ok(new { newAccessToken, newRefreshToken });
    }

    private bool IsValidUser(string login, string password) =>
        // Тут, либо обращение к микросервису пользователя и проверки через него валидности данных для пользователя
        // либо обращение к сервису пользователя. Пока вообще ничего не понимаю.
        true; // Заглушка для примера
}