using AutoMapper;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Globalization;

namespace HackerNews.mapper
{
    public class NewsProfile: Profile
    {
        private IFormatProvider persianCulture;

        public NewsProfile()
            {

            CreateMap<Model.HackerNews, Model.News>()
                .ForMember(dest => dest.PostedBy, src => src.MapFrom(src => src.by))
                .ForMember(dest => dest.CommentCount, src => src.MapFrom(src => src.descendants))
                .ForMember(dest => dest.Time, src => src.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.time)));
            }
    }
}
