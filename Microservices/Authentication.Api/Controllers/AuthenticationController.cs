using Entities;
using Entities.MicroServices.Models;
using Microsoft.AspNetCore.Mvc;
using Services.ServiceInterfaces;

namespace Authentication.Api.Controllers;

[Route("Authentication")]
public class AuthenticationController(IConfiguration configuration, ITokenService tokenService):ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // Проверка логина и пароля пользователя
        if (!IsValidUser(model.Username, model.Password, model.Email))
            return Unauthorized();

        // Создание access токена
        var accessToken = tokenService.GenerateToken(configuration["Jwt:SecretKey"]!, 5, model.Email, Role.User);

        // Создание Refresh Token
        var refreshToken = tokenService.GenerateToken(configuration["Jwt:SecretKey"]!, 180, model.Email, Role.User);

        try
        {
           await tokenService.TryAddTokenToDataBaseAsync(refreshToken);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

        return Ok(new { Token = accessToken, RefreshToken = refreshToken });
    }

    [HttpPost]
    [Route("updateAccessToken")]
    public async Task<IActionResult> UpdateAccessToken([FromBody] RefreshTokensModel refreshTokensModel)
    {
        if (!await tokenService.IsValidRefreshTokenAsync(refreshTokensModel.RefreshToken, configuration["Jwt:SecretKey"]!))
            return Unauthorized();

        var email = tokenService.GetEmailFromToken(refreshTokensModel.RefreshToken, configuration["Jwt:SecretKey"]!);
        var role = tokenService.GetRoleFromToken(refreshTokensModel.RefreshToken, configuration["Jwt:SecretKey"]!);
        var newAccessToken = tokenService.GenerateToken(configuration["Jwt:SecretKey"]!, 5, email, role);

        return Ok(new { Token = newAccessToken, refreshTokensModel.RefreshToken });
    }

    private bool IsValidUser(string login, string password, string email)
    {
        //тут типо, либо обращение к микросервису пользователя и проверки через него валидности данных для пользователя
        //либо обращение к сервису пользователя. Пока вообще ничего не понимаю.

        return true; // Заглушка для примера
    }
}