using System.Text.Json.Serialization;

namespace Kaonavi.Net.Entities
{
    /// <summary>アクセストークン</summary>
    public record Token
    {
        /// <summary>
        /// Tokenの新しいインスタンスを生成します。
        /// </summary>
        /// <param name="accessToken"><inheritdoc cref="AccessToken" path="/summary"/></param>
        /// <param name="tokenType"><inheritdoc cref="TokenType" path="/summary"/></param>
        /// <param name="expiresIn"><inheritdoc cref="ExpiresIn" path="/summary"/></param>
        public Token(string accessToken, string tokenType, int expiresIn)
            => (AccessToken, TokenType, ExpiresIn) = (accessToken, tokenType, expiresIn);

        /// <summary>アクセストークン</summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; }

        /// <summary>トークン種別(<c>"Bearer"</c>固定)</summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; init; }

        /// <summary>トークンの有効期限 (秒)</summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; init; }
    }
}
