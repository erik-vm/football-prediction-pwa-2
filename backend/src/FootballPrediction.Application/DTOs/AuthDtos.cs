namespace FootballPrediction.Application.DTOs;

public record RegisterRequest(string Username, string Email, string Password);

public record LoginRequest(string Email, string Password);

public record AuthResponse(string AccessToken, string RefreshToken, UserDto User);

public record RefreshRequest(string RefreshToken);

public record ChangePasswordRequest(string CurrentPassword, string NewPassword);

public record UserDto(Guid Id, string Username, string Email, string Role);
