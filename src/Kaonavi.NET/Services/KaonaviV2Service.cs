using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Kaonavi.Net.Entities;
using Kaonavi.Net.Entities.Api;

namespace Kaonavi.Net.Services
{
    public class KaonaviV2Service
    {
        private const string BaseApiAddress = "https://api.kaonavi.jp/api/v2.0";
        private readonly HttpClient _client;
        private readonly string _consumerKey;
        private readonly string _consumerSecret;

        private const string TokenHeader = "Kaonavi-Token";
        public string? AccessToken
        {
            get => _client.DefaultRequestHeaders.TryGetValues(TokenHeader, out var values) ? values.First() : null;
            set
            {
                _client.DefaultRequestHeaders.Remove(TokenHeader);
                if (!string.IsNullOrWhiteSpace(value))
                    _client.DefaultRequestHeaders.Add(TokenHeader, value);
            }
        }

        public KaonaviV2Service(HttpClient client, string consumerKey, string consumerSecret)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _consumerKey = consumerKey ?? throw new ArgumentNullException(nameof(consumerKey));
            _consumerSecret = consumerSecret ?? throw new ArgumentNullException(nameof(consumerSecret));

            _client.BaseAddress ??= new(BaseApiAddress);
        }

        public async Task<Token> AuthenticateAsync(CancellationToken cancellationToken = default)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes($"{_consumerKey}:{_consumerSecret}");
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "grant_type", "client_credentials" }
            });
            _client.DefaultRequestHeaders.Authorization = new("Basic", Convert.ToBase64String(byteArray));

            var response = await _client.PostAsync("/token", content, cancellationToken).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw await CreateApiExceptionAsync(ex, response.Content).ConfigureAwait(false);
            }

            var token = await response.Content
                .ReadFromJsonAsync<Token>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            _client.DefaultRequestHeaders.Authorization = null;
            return token!;
        }

        public async Task<MemberLayout> FetchMemberLayoutAsync(CancellationToken cancellationToken = default)
        {
            AccessToken ??= (await AuthenticateAsync(cancellationToken).ConfigureAwait(false))?.AccessToken;

            var response = await _client.GetAsync("/member_layouts").ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw await CreateApiExceptionAsync(ex, response.Content).ConfigureAwait(false);
            }

            return (await response.Content
                .ReadFromJsonAsync<MemberLayout>(cancellationToken: cancellationToken)
                .ConfigureAwait(false))!;
        }

        public record ErrorResponse([property: JsonPropertyName("errors")] IEnumerable<string> Errors);
        private static async Task<ApplicationException> CreateApiExceptionAsync(HttpRequestException ex, HttpContent content)
        {
            string errorMessage = content.Headers.ContentType.MediaType == "application/json"
                ? string.Join("\n", (await content.ReadFromJsonAsync<ErrorResponse>().ConfigureAwait(false))!.Errors)
                : await content.ReadAsStringAsync().ConfigureAwait(false);
            return new ApplicationException(errorMessage, ex);
        }
    }
}
