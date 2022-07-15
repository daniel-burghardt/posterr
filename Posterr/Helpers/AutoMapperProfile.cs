using AutoMapper;
using Posterr.DTOs.Posts;
using Posterr.DTOs.Users;
using Posterr.Models.Posts;
using Posterr.Models.Users;

namespace Posterr.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			// Users
			CreateMap<User, UserDto>();
			CreateMap<User, UserProfileDto>()
				.ForMember(x => x.JoinedOn, x => x.MapFrom(z => z.JoinedOn.ToString("MMMM d, yyyy")));

			// Posts
			CreateMap<Post, PostDto>()
				.ForMember(x => x.Type, x => x.MapFrom(z => z.Discriminator))
				.IncludeAllDerived();
			CreateMap<OriginalPost, PostDto.OriginalPostDto>();
			CreateMap<QuotePost, PostDto.QuotePostDto>();
			CreateMap<Repost, PostDto.RepostDto>();

			CreateMap<CreateOriginalPostDto, OriginalPost>();
			CreateMap<CreateQuotePostDto, QuotePost>();
			CreateMap<CreateRepostDto, Repost>();
		}
	}
}
