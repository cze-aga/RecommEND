using AutoMapper;
using REcommEND.Models;
using REcommEND.Models.Dto;
using System;
using System.Globalization;

namespace REcommEND.Data.Mappings
{
    public class IMDBMoviesMapperProfile : Profile
    {
        public IMDBMoviesMapperProfile()
        {
            CreateMap<IMDBMovieDto, Movie>()
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(x => x.Released, opt => opt.MapFrom(src => DateTime.Parse(src.Released)))
                .ForMember(x => x.Plot, opt => opt.MapFrom(src => src.Plot))
                .ForMember(x => x.IMDBRating, opt => opt.MapFrom(src => Convert.ToDecimal(src.ImdbRating, CultureInfo.InvariantCulture.NumberFormat)))
                .ForMember(x => x.IMDBVotesNumber, opt => opt.MapFrom(src => Convert.ToInt32(src.ImdbVotes.Replace(",", "").Replace(".",""), CultureInfo.InvariantCulture.NumberFormat)))
                .ForMember(x => x.Poster, opt => opt.MapFrom(src => src.Poster)).ReverseMap();
        }
    }
}
