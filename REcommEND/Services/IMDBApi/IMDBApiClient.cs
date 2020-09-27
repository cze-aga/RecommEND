using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using REcommEND.Models;
using REcommEND.Models.Dto;
using REcommEND.Services.Configuration;
using System.Net.Http;

namespace REcommEND.Services.IMDBApi
{
    public class IMDBApiClient : IIMDBApiClient
    {
        private readonly IOptions<IMDBConfigurationOptions> _optionsAccessor;
        // Create a field to store the mapper object
        private readonly IMapper _mapper;

        public IMDBApiClient(IOptions<IMDBConfigurationOptions> options, IMapper mapper)
        {
            this._optionsAccessor = options;
            this._mapper = mapper;
        }

        public async void GetMovies()
        {
            using var client = new HttpClient();
            var apikey = this._optionsAccessor.Value.apiKey;
            var result = await client.GetAsync($"http://www.omdbapi.com/?apikey={apikey}&t=1");
            var json = await result.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<IMDBMovieDto>(json);

            var model = _mapper.Map<Movie>(movie);
        }



    }
}
